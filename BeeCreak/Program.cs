using BeeCreak.Shared.Services.Dynamic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;

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
                services.AddSingleton<GameManager>();
                services.AddSingleton<GameFactory>();
                services.AddSingleton<UserDataManager>();
                services.AddSingleton<SceneManager>();
                services.AddSingleton<AppContext>();

                services.AddScoped<CellFactory>();
                services.AddScoped<TileFactory>();
                services.AddScoped<EntityFactory>();

                // Register scenes
                services.AddScoped<MenuScene>();
                services.AddScoped<GameScene>();
                services.AddScoped<IntroScene>();

                // Register components
                services.AddScoped<TextComponent>();
                services.AddScoped<MainMenu>();
                services.AddScoped<LoadMenuComponent>();
                services.AddScoped<TileMapComponent>();
                services.AddScoped<EntityComponentCollection>();

                // Register managers and services
                services.AddScoped<CellManager>();
                services.AddScoped<EntityManager>();
                services.AddScoped<TileManager>();

                // Register other components
                services.AddScoped<TileVariator>();
                services.AddScoped<PlayerBehavior>();
                services.AddScoped<Camera>();
                services.AddScoped<Player>();

                using var sceneManager = new BeeCreak(services);

                sceneManager.Run();
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