using BeeCreak.Scene.Main;
using Microsoft.Extensions.DependencyInjection;
using BeeCreak.Shared.Services.Static;
using BeeCreak.Shared.Services.Dynamic;
using BeeCreak.Scene.Menu;

var services = new ServiceCollection();

// Services
services.AddSingleton<BeeCreak.BeeCreak>();
services.AddSingleton<ISound, Sound>();
services.AddSingleton<IInput, Input>();
services.AddScoped<IShapeRouter, ShapeRouter>();

// Scene 

services.AddScoped<MenuScene>();
services.AddSingleton<ICamera, Camera>();

// Transient
services.AddTransient<ControlBehavior>();


using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

scope.ServiceProvider.GetRequiredService<BeeCreak.BeeCreak>().Run();
