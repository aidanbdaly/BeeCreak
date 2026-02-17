namespace BeeCreak.Game.Server;

public sealed record ServerOptions
{
    public int Port { get; init; } = 7777;
    public int TickRate { get; init; } = 60;
    public int MaxClients { get; init; } = 64;
    public int TimeoutSeconds { get; init; } = 30;

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
        Console.WriteLine("  dotnet run -- --port 7777 --tickRate 60 --maxClients 64 --timeoutSeconds 30");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  --port             UDP listen port (default 7777)");
        Console.WriteLine("  --tickRate         Server tick rate in Hz (default 60)");
        Console.WriteLine("  --maxClients       Maximum concurrent sessions (default 64)");
        Console.WriteLine("  --timeoutSeconds   Session timeout in seconds (default 30)");
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
}

public readonly record struct ServerOptionsResult(ServerOptions? Options, string? ErrorMessage = null, bool ShowHelp = false)
{
    public bool IsValid => ErrorMessage is null;
}
