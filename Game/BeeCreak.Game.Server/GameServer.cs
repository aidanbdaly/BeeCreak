using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace BeeCreak.Game.Server;

public sealed class GameServer
{
    private readonly ServerOptions _options;
    private readonly ILogger<GameServer> _logger;
    private readonly Channel<UdpReceiveResult> _receiveChannel;
    private readonly Dictionary<Guid, Session> _sessions = new();
    private readonly Dictionary<string, Guid> _endpointIndex = new(StringComparer.Ordinal);
    private readonly UdpClient _udpClient;

    public GameServer(ServerOptions options, ILogger<GameServer> logger)
    {
        _options = options;
        _logger = logger;
        _udpClient = new UdpClient(options.Port)
        {
            EnableBroadcast = false
        };
        _udpClient.Client.ReceiveBufferSize = 1024 * 1024;
        _udpClient.Client.SendBufferSize = 1024 * 1024;

        _receiveChannel = Channel.CreateUnbounded<UdpReceiveResult>(new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = true
        });
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting UDP server on port {Port} at {TickRate} Hz.", _options.Port, _options.TickRate);
        _logger.LogInformation("Max clients: {MaxClients}, Session timeout: {TimeoutSeconds}s.", _options.MaxClients, _options.TimeoutSeconds);
        if (!string.IsNullOrWhiteSpace(_options.ServerId))
        {
            _logger.LogInformation("Server id: {ServerId}.", _options.ServerId);
        }

        var receiveTask = ReceiveLoopAsync(cancellationToken);
        var loopTask = GameLoopAsync(cancellationToken);

        try
        {
            await Task.WhenAll(receiveTask, loopTask);
        }
        finally
        {
            _receiveChannel.Writer.TryComplete();
            _udpClient.Dispose();
            _logger.LogInformation("Shutting down.");
        }
    }

    private async Task ReceiveLoopAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var result = await _udpClient.ReceiveAsync(cancellationToken);
                await _receiveChannel.Writer.WriteAsync(result, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (ObjectDisposedException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Receive error.");
                try
                {
                    await Task.Delay(200, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }

    private async Task GameLoopAsync(CancellationToken cancellationToken)
    {
        var tickDuration = TimeSpan.FromSeconds(1.0 / _options.TickRate);
        var timeout = TimeSpan.FromSeconds(_options.TimeoutSeconds);
        var stopwatch = Stopwatch.StartNew();
        var last = stopwatch.Elapsed;
        var accumulator = TimeSpan.Zero;

        while (!cancellationToken.IsCancellationRequested)
        {
            var now = stopwatch.Elapsed;
            var delta = now - last;
            last = now;
            accumulator += delta;

            while (accumulator >= tickDuration)
            {
                ProcessPackets();
                ExpireSessions(timeout);
                accumulator -= tickDuration;
            }

            var sleep = tickDuration - accumulator;

            if (sleep > TimeSpan.Zero)
            {
                try
                {
                    await Task.Delay(sleep, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }

    private void ProcessPackets()
    {
        while (_receiveChannel.Reader.TryRead(out var packet))
        {
            HandlePacket(packet);
        }
    }

    private void HandlePacket(UdpReceiveResult packet)
    {
        var text = Encoding.UTF8.GetString(packet.Buffer);
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        var parts = text.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (parts.Length == 0)
        {
            return;
        }

        var command = parts[0].ToUpperInvariant();
        switch (command)
        {
            case "HELLO":
                HandleHello(packet.RemoteEndPoint);
                break;
            case "PING":
                HandlePing(packet.RemoteEndPoint, parts);
                break;
            default:
                SendError(packet.RemoteEndPoint, "UNKNOWN_COMMAND");
                break;
        }
    }

    private void HandleHello(IPEndPoint remoteEndPoint)
    {
        var endpointKey = EndpointKey(remoteEndPoint);
        if (_endpointIndex.TryGetValue(endpointKey, out var existingSessionId)
            && _sessions.TryGetValue(existingSessionId, out var existing))
        {
            existing.LastSeenUtc = DateTime.UtcNow;
            SendWelcome(remoteEndPoint, existing.Id);
            return;
        }

        if (_sessions.Count >= _options.MaxClients)
        {
            SendError(remoteEndPoint, "SERVER_FULL");
            return;
        }

        var session = new Session(Guid.NewGuid(), remoteEndPoint);
        _sessions[session.Id] = session;
        _endpointIndex[endpointKey] = session.Id;
        _logger.LogInformation("New session {SessionId} from {RemoteEndPoint}.", session.Id, remoteEndPoint);
        SendWelcome(remoteEndPoint, session.Id);
    }

    private void HandlePing(IPEndPoint remoteEndPoint, string[] parts)
    {
        if (parts.Length < 3)
        {
            SendError(remoteEndPoint, "INVALID_PING");
            return;
        }

        if (!Guid.TryParse(parts[1], out var sessionId))
        {
            SendError(remoteEndPoint, "INVALID_SESSION");
            return;
        }

        if (!_sessions.TryGetValue(sessionId, out var session))
        {
            SendError(remoteEndPoint, "UNKNOWN_SESSION");
            return;
        }

        var endpointKey = EndpointKey(remoteEndPoint);
        if (!string.Equals(endpointKey, EndpointKey(session.RemoteEndPoint), StringComparison.Ordinal))
        {
            SendError(remoteEndPoint, "ENDPOINT_MISMATCH");
            return;
        }

        if (!uint.TryParse(parts[2], out var sequence))
        {
            SendError(remoteEndPoint, "INVALID_SEQUENCE");
            return;
        }

        session.LastSeenUtc = DateTime.UtcNow;
        session.LastSequence = sequence;

        SendText(remoteEndPoint, $"PONG|{sequence}");
    }

    private void ExpireSessions(TimeSpan timeout)
    {
        if (_sessions.Count == 0)
        {
            return;
        }

        var now = DateTime.UtcNow;
        List<Guid>? expired = null;

        foreach (var (sessionId, session) in _sessions)
        {
            if (now - session.LastSeenUtc > timeout)
            {
                expired ??= new List<Guid>();
                expired.Add(sessionId);
            }
        }

        if (expired is null)
        {
            return;
        }

        foreach (var sessionId in expired)
        {
            if (_sessions.Remove(sessionId, out var session))
            {
                _endpointIndex.Remove(EndpointKey(session.RemoteEndPoint));
                _logger.LogInformation("Session {SessionId} timed out.", sessionId);
            }
        }
    }

    private void SendWelcome(IPEndPoint remoteEndPoint, Guid sessionId)
    {
        SendText(remoteEndPoint, $"WELCOME|{sessionId}|{_options.TickRate}");
    }

    private void SendError(IPEndPoint remoteEndPoint, string code)
    {
        SendText(remoteEndPoint, $"ERROR|{code}");
    }

    private void SendText(IPEndPoint remoteEndPoint, string payload)
    {
        var data = Encoding.UTF8.GetBytes(payload);
        try
        {
            _udpClient.SendAsync(data, data.Length, remoteEndPoint);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Send error to {RemoteEndPoint}.", remoteEndPoint);
        }
    }

    private static string EndpointKey(EndPoint endPoint)
    {
        return endPoint.ToString() ?? "";
    }

    private sealed class Session
    {
        public Session(Guid id, IPEndPoint remoteEndPoint)
        {
            Id = id;
            RemoteEndPoint = remoteEndPoint;
            LastSeenUtc = DateTime.UtcNow;
            LastSequence = 0;
        }

        public Guid Id { get; }
        public IPEndPoint RemoteEndPoint { get; }
        public DateTime LastSeenUtc { get; set; }
        public uint LastSequence { get; set; }
    }
}
