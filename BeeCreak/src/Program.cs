using BeeCreak;
using BeeCreak.App;
using BeeCreak.Components.Button;
using BeeCreak.Features.Game.Tile;
using BeeCreak.Features.Menu;
using BeeCreak.Game;
using BeeCreak.Game.Scene;
using BeeCreak.Game.Scene.Entity;
using BeeCreak.Game.Scene.Light;
using BeeCreak.Game.Scene.Tile;
using BeeCreak.Game.State;
using BeeCreak.Generation;
using BeeCreak.Tools.Dynamic;
using BeeCreak.Tools.Dynamic.Input;
using BeeCreak.Tools.Static;
using BeeCreak.UI;
using BeeCreak.UI.Components;
using BeeCreak.Utilities.Static;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddSingleton<BeeCreak.BeeCreak>();
services.AddSingleton<IApp, App>();
services.AddSingleton<ISprite, Sprite>();
services.AddSingleton<ISound, Sound>();
services.AddSingleton<IInput, Input>();
services.AddScoped<IEventManager, EventManager>();
services.AddScoped<IUISettings, UISettings>();

// Textures
services.AddSingleton<ISpriteSheetManager, SpriteSheetManager>();
services.AddSingleton<ITileVariantCalculator, TileVariantCalculator>();
services.AddSingleton<ITileAtlas, TileAtlas>();
services.AddSingleton<IButtonAtlas, ButtonAtlas>();

services.AddSingleton<IShapeRouter, ShapeRouter>();

// Save
services.AddScoped<ISaveManager, SaveManager>();
services.AddScoped<GameFactory>();
services.AddScoped<CellFactory>();
services.AddScoped<LightFactory>();
services.AddScoped<TileMapFactory>();
services.AddScoped<EntityFactory>();

// Scene Nodes
services.AddScoped<GameScene>();
services.AddScoped<MenuScene>();
services.AddScoped<CellManager>();
services.AddScoped<UIManager>();
services.AddScoped<TileManager>();
services.AddScoped<LightManager>();
services.AddScoped<EntityManager>();

// Transient
services.AddTransient<IButton, Button>();
services.AddTransient<MenuActions>();
services.AddTransient<ITileMap, TileMap>();


using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

scope.ServiceProvider.GetRequiredService<BeeCreak.BeeCreak>().Run();
