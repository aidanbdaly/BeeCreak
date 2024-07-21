using BeeCreak.Run;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddScoped<IContext, Context>();
services.AddTransient<BeeCreak.Run.BeeCreak>();

using var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

scope.ServiceProvider.GetRequiredService<BeeCreak.Run.BeeCreak>().Run();
