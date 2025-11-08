using BeeCreak.Core;
using BeeCreak.App.Intro;
using Microsoft.Extensions.DependencyInjection;

namespace BeeCreak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var services = new ServiceCollection();

                services.AddSingleton<BeeCreak>();
                
                var serviceProvider = services.BuildServiceProvider();

                using (var game = serviceProvider.GetRequiredService<BeeCreak>())
                {
                    game.Run();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                Environment.Exit(1);
            }
        }
    }
}