using BeeCreak.Scene.Main;
using Microsoft.Extensions.DependencyInjection;
using BeeCreak.Shared.Services.Static;
using BeeCreak.Shared.Services.Dynamic;

var services = new ServiceCollection();

// Services
services.AddSingleton<BeeCreak.BeeCreak>();
services.AddSingleton<ISound, Sound>();
services.AddSingleton<Player>();
services.AddScoped<DungeonGenerator>();

// Scene 
services.AddSingleton<Camera>();



using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

scope.ServiceProvider.GetRequiredService<BeeCreak.BeeCreak>().Run();
