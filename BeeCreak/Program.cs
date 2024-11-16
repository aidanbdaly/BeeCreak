using BeeCreak;
using BeeCreak.Game;
using BeeCreak.Game.Scene;
using BeeCreak.Game.Scene.Entity;
using BeeCreak.Game.Scene.Light;
using BeeCreak.Game.Scene.Tile;
using BeeCreak.Menu;
using BeeCreak.Tools;
using BeeCreak.Tools.Dynamic;
using BeeCreak.Tools.Dynamic.Input;
using BeeCreak.Tools.Static;
using BeeCreak.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

var services = new ServiceCollection();

services.AddSingleton<BeeCreak.BeeCreak>();
services.AddSingleton<IAppRouter, AppRouter>();

services.AddSingleton<ISound, Sound>();
services.AddSingleton<IInput, Input>();
services.AddSingleton<ISprite, Sprite>();

services.AddScoped<ISaveManager, SaveManager>();
services.AddScoped<IEventManager, EventManager>();
services.AddScoped<IUISettings, UISettings>();

// App Nodes
services.AddScoped<GameManager>();
services.AddScoped<Main>();
services.AddScoped<Load>();
services.AddScoped<Settings>();

// Scene Nodes
services.AddScoped<CellManager>();
services.AddScoped<UIManager>();
services.AddScoped<TileManager>();
services.AddScoped<LightManager>();
services.AddScoped<EntityManager>();

using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

scope.ServiceProvider.GetRequiredService<BeeCreak.BeeCreak>().Run();
