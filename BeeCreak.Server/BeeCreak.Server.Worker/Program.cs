using BeeCreak.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

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

var serverOptions = optionsResult.Options!;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(kestrel =>
{
    kestrel.ListenAnyIP(serverOptions.HealthPort);
});
builder.Services.AddSingleton(serverOptions);
builder.Services.AddSingleton<GameServer>();
builder.Services.AddHostedService<GameServerWorker>();

var app = builder.Build();
app.MapGet(serverOptions.HeartbeatPath, () => Results.Ok(new { status = "ok" }));

await app.RunAsync();
