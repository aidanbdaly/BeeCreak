namespace BeeCreak.Game.Server;

public sealed class GameServerWorker : BackgroundService
{
    private readonly GameServer _server;
    private readonly ILogger<GameServerWorker> _logger;

    public GameServerWorker(GameServer server, ILogger<GameServerWorker> logger)
    {
        _server = server;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Game server worker starting.");
        await _server.RunAsync(stoppingToken);
        _logger.LogInformation("Game server worker stopped.");
    }
}
