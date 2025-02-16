using BeeCreak.App;
using BeeCreak.Scene.Main;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

using System.IO;
using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Services.Static;
using BeeCreak.Shared.Services.Dynamic;
using BeeCreak.Shared.Data.Models;
using BeeCreak.Scene.Menu;
using BeeCreak.Shared.UI;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

services.Configure<AppSettings>(configuration.GetSection("Asset"));
services.AddSingleton<IUISettings, UISettings>();

// Services
services.AddSingleton<BeeCreak.BeeCreak>();
services.AddSingleton<ISceneController, SceneController>();
services.AddSingleton<ISpriteController, SpriteController>();
services.AddSingleton<ISound, Sound>();
services.AddSingleton<IInput, Input>();

services.AddScoped<IShapeRouter, ShapeRouter>();

// Asset
services.AddSingleton<ITileAssetProvider, TileAssetProvider>();
services.AddSingleton<IButtonAssetProvider, ButtonAssetProvider>();
services.AddSingleton<IVisualAssetProvider, VisualAssetProvider>();
services.AddSingleton<IGameProvider, GameProvider>();


// Scene 
services.AddScoped<GameScene>();
services.AddScoped<MenuScene>();
services.AddScoped<CellController>();
services.AddScoped<HUDController>();
services.AddScoped<LightManager>();

services.AddSingleton<ICamera, Camera>();

// Transient
services.AddTransient<IButton, Button>();
services.AddTransient<MenuActions>();
services.AddTransient<ITileMap, TileMap>();
services.AddTransient<ITileVariantCalculator, TileVariantCalculator>();
services.AddTransient<ITile, Tile>();
services.AddTransient<ITime, Time>();
services.AddTransient<Player>();
services.AddTransient<IControlBehavior, ControlBehavior>();


using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

scope.ServiceProvider.GetRequiredService<BeeCreak.BeeCreak>().Run();
