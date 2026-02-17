using BeeCreak.Game.Server;

var optionsResult = ServerOptions.Parse(args);
if (optionsResult.ShowHelp)
{
    ServerOptions.PrintUsage();
    return;
}

if (!optionsResult.IsValid)
{
    Console.Error.WriteLine(optionsResult.ErrorMessage);
    Console.Error.WriteLine();
    ServerOptions.PrintUsage();
    Environment.ExitCode = 1;
    return;
}

var options = optionsResult.Options!;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton(options);
builder.Services.AddSingleton<GameServer>();
builder.Services.AddHostedService<GameServerWorker>();

using var host = builder.Build();
await host.RunAsync();
