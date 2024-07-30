using BeeCreak.Run;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddScoped<IToolCollection, ToolCollection>();
services.AddTransient<BeeCreak.BeeCreak>();

using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

scope.ServiceProvider.GetRequiredService<BeeCreak.BeeCreak>().Run();
