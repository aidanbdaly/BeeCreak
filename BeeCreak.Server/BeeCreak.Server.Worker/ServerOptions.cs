namespace BeeCreak.Server;

public sealed record ServerOptions
{
    public int Port { get; init; } = 7777;
    public int TickRate { get; init; } = 60;
    public int MaxClients { get; init; } = 64;
    public int TimeoutSeconds { get; init; } = 30;
    public int HealthPort { get; init; } = 8080;
    public string HeartbeatPath { get; init; } = "/heartbeat";
    public string? ServerId { get; init; }

    public static ServerOptionsResult Parse(string[] args)
    {
        var options = new ServerOptions();
        string? errorMessage = null;

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            switch (arg)
            {
                case "--help":
                case "-h":
                    return new ServerOptionsResult(options, ShowHelp: true);
                case "--port":
                    if (!TryReadInt(args, ref i, out var port, out errorMessage))
                    {
                        return new ServerOptionsResult(options, errorMessage);
                    }

                    if (port is < 1 or > 65535)
                    {
                        return new ServerOptionsResult(options, "Port must be between 1 and 65535.");
                    }

                    options = options with { Port = port };
                    break;
                case "--tickRate":
                    if (!TryReadInt(args, ref i, out var tickRate, out errorMessage))
                    {
                        return new ServerOptionsResult(options, errorMessage);
                    }

                    if (tickRate is < 1 or > 240)
                    {
                        return new ServerOptionsResult(options, "Tick rate must be between 1 and 240.");
                    }

                    options = options with { TickRate = tickRate };
                    break;
                case "--maxClients":
                    if (!TryReadInt(args, ref i, out var maxClients, out errorMessage))
                    {
                        return new ServerOptionsResult(options, errorMessage);
                    }

                    if (maxClients is < 1 or > 10000)
                    {
                        return new ServerOptionsResult(options, "Max clients must be between 1 and 10000.");
                    }

                    options = options with { MaxClients = maxClients };
                    break;
                case "--healthPort":
                case "--health-port":
                    if (!TryReadInt(args, ref i, out var healthPort, out errorMessage))
                    {
                        return new ServerOptionsResult(options, errorMessage);
                    }

                    if (healthPort is < 1 or > 65535)
                    {
                        return new ServerOptionsResult(options, "Health port must be between 1 and 65535.");
                    }

                    options = options with { HealthPort = healthPort };
                    break;
                case "--heartbeatPath":
                case "--heartbeat-path":
                    if (!TryReadString(args, ref i, out var heartbeatPath, out errorMessage))
                    {
                        return new ServerOptionsResult(options, errorMessage);
                    }

                    if (string.IsNullOrWhiteSpace(heartbeatPath))
                    {
                        return new ServerOptionsResult(options, "Heartbeat path cannot be empty.");
                    }

                    if (!heartbeatPath.StartsWith("/", StringComparison.Ordinal))
                    {
                        heartbeatPath = "/" + heartbeatPath;
                    }

                    options = options with { HeartbeatPath = heartbeatPath };
                    break;
                case "--session-id":
                case "--server-id":
                case "--serverId":
                    if (!TryReadString(args, ref i, out var serverId, out errorMessage))
                    {
                        return new ServerOptionsResult(options, errorMessage);
                    }

                    if (string.IsNullOrWhiteSpace(serverId))
                    {
                        return new ServerOptionsResult(options, "Server id cannot be empty.");
                    }

                    options = options with { ServerId = serverId };
                    break;
                case "--timeoutSeconds":
                    if (!TryReadInt(args, ref i, out var timeoutSeconds, out errorMessage))
                    {
                        return new ServerOptionsResult(options, errorMessage);
                    }

                    if (timeoutSeconds is < 5 or > 300)
                    {
                        return new ServerOptionsResult(options, "Timeout seconds must be between 5 and 300.");
                    }

                    options = options with { TimeoutSeconds = timeoutSeconds };
                    break;
                default:
                    return new ServerOptionsResult(options, $"Unknown argument: {arg}");
            }
        }

        return new ServerOptionsResult(options, errorMessage);
    }

    public static void PrintUsage()
    {
        Console.WriteLine("BeeCreak Game Server (UDP)");
        Console.WriteLine();
        Console.WriteLine("Usage:");
        Console.WriteLine("  dotnet run -- --port 7777 --tickRate 60 --maxClients 64 --timeoutSeconds 30 --health-port 8080 --heartbeat-path /heartbeat --session-id <id>");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  --port             UDP listen port (default 7777)");
        Console.WriteLine("  --tickRate         Server tick rate in Hz (default 60)");
        Console.WriteLine("  --maxClients       Maximum concurrent sessions (default 64)");
        Console.WriteLine("  --timeoutSeconds   Session timeout in seconds (default 30)");
        Console.WriteLine("  --health-port      Health HTTP listen port (default 8080)");
        Console.WriteLine("  --heartbeat-path   Health endpoint path (default /heartbeat)");
        Console.WriteLine("  --session-id       Server id (orchestrator-provided)");
        Console.WriteLine("  --help, -h         Show this help");
    }

    private static bool TryReadInt(string[] args, ref int index, out int value, out string? errorMessage)
    {
        value = 0;
        errorMessage = null;

        if (index + 1 >= args.Length)
        {
            errorMessage = $"Missing value for {args[index]}";
            return false;
        }

        if (!int.TryParse(args[index + 1], out value))
        {
            errorMessage = $"Invalid integer for {args[index]}";
            return false;
        }

        index++;
        return true;
    }

    private static bool TryReadString(string[] args, ref int index, out string value, out string? errorMessage)
    {
        value = string.Empty;
        errorMessage = null;

        if (index + 1 >= args.Length)
        {
            errorMessage = $"Missing value for {args[index]}";
            return false;
        }

        value = args[index + 1];
        index++;
        return true;
    }
}

public readonly record struct ServerOptionsResult(ServerOptions? Options, string? ErrorMessage = null, bool ShowHelp = false)
{
    public bool IsValid => ErrorMessage is null;
}
