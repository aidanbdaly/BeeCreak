using BeeCreak.Scene.Main;
using Microsoft.Extensions.DependencyInjection;
using BeeCreak.Shared.Services.Dynamic;

var services = new ServiceCollection();

services.AddSingleton<Game>();
services.AddScoped<MenuScene>();
services.AddScoped<GameScene>();
services.AddScoped<MainMenuComponent>();
services.AddScoped<LoadMenuComponent>();
services.AddScoped<EntityLayerComponent>();
services.AddScoped<CellManager>();
services.AddScoped<TileMapComponent>();
services.AddScoped<TileVariator>();
services.AddScoped<PlayerBehavior>();
services.AddScoped<EntityManager>();
services.AddScoped<TileManager>();
services.AddScoped<Camera>();
services.AddScoped<Player>();


var game = new BeeCreak.BeeCreak(services);

game.Run();