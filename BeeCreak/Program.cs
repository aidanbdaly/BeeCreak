using BeeCreak.Scene.Main;
using Microsoft.Extensions.DependencyInjection;
using BeeCreak.Shared.Services.Static;
using BeeCreak.Shared.Services.Dynamic;
using BeeCreak.Shared.Services;

var services = new ServiceCollection();

// Services
services.AddSingleton<BeeCreak.BeeCreak>();
services.AddSingleton<ISound, Sound>();

// Global Services
services.AddSingleton<AssetManager>();

services.AddSingleton(provider => services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>());


// Game Scene Services
services.AddSingleton<Camera>();
services.AddSingleton<Player>();
services.AddScoped<DungeonGenerator>();

// Menu Scene Services
services.AddScoped<MenuScene>();



using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

scope.ServiceProvider.GetRequiredService<BeeCreak.BeeCreak>().Run();
