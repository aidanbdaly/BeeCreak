using BeeCreak.Core;
using BeeCreak.Game;
using BeeCreak.Intro;
using BeeCreak.Menu;

namespace BeeCreak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using var app = new App();

                app.RegisterScene("MenuScene", () => new MenuScene(app));
                app.RegisterScene("IntroScene", () => new IntroScene(app));
                app.RegisterScene("PlayScene", () => new GameScene(app));

                app.Run();
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