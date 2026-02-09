using BeeCreak.Engine;
using BeeCreak.Engine.Data.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Game.Home.Services
{
    public record Button(
        string Label,
        SpriteFont Font,
        SpriteSheet Style,
        Action OnClick,
        Action? OnHover = null
    );

    public record Menu(
        Button[] Buttons,
        int Gap
    );   

    public enum HomeMenu
    {
        Overview,
        NewGame,
        LoadGame,
        Settings
    }

    public class MenuFactory(App app)
    {
        private readonly Dictionary<HomeMenu, Func<App, Menu>> factories =
        new()
        {
            {
                HomeMenu.Overview,
                (app) =>
                {
                    HomeContext homeContext = app.Services.GetService<HomeContext>()
                        ?? throw new InvalidOperationException("HomeContext not found");

                    Button[] buttons = [];

                    Button newGame = new(
                        "New Game",
                        app.Content.Load<SpriteFont>("Font/lookout"),
                        app.Content.Load<SpriteSheet>("SpriteSheet/menu_button"),
                        () =>
                        {
                            app.SceneManager.Stage("Play");
                            app.SceneManager.Reveal();
                        });

                    Button loadGame = new(
                        "Load Game",
                        app.Content.Load<SpriteFont>("Font/lookout"),
                        app.Content.Load<SpriteSheet>("SpriteSheet/menu_button"),
                        () => homeContext.ChangeMenu(HomeMenu.Overview));

                    Button settings = new(
                        "Settings",
                        app.Content.Load<SpriteFont>("Font/lookout"),
                        app.Content.Load<SpriteSheet>("SpriteSheet/menu_button"),
                        () => homeContext.ChangeMenu(HomeMenu.Overview));

                    buttons = [.. buttons, newGame, loadGame, settings];

                    return new Menu(buttons, 16);
                }
            },
            {
                HomeMenu.NewGame,
                (app) =>
                {
                    HomeContext homeContext = app.Services.GetService<HomeContext>()
                        ?? throw new InvalidOperationException("HomeContext not found");

                    Button[] buttons = [];

                    Button back = new(
                        "Back",
                        app.Content.Load<SpriteFont>("Font/lookout"),
                        app.Content.Load<SpriteSheet>("SpriteSheet/menu_button"),
                        () => homeContext.ChangeMenu(HomeMenu.Overview));

                    buttons = [.. buttons, back];

                    return new Menu(buttons, 16);
                }
            },
            {
                HomeMenu.LoadGame,
                (app) =>
                {
                    HomeContext homeContext = app.Services.GetService<HomeContext>()
                        ?? throw new InvalidOperationException("HomeContext not found");

                    Button[] buttons = [];

                    Button back = new(
                        "Back",
                        app.Content.Load<SpriteFont>("Font/lookout"),
                        app.Content.Load<SpriteSheet>("SpriteSheet/menu_button"),
                        () => homeContext.ChangeMenu(HomeMenu.Overview));

                    buttons = [.. buttons, back];

                    return new Menu(buttons, 16);
                }
            },
            {
                HomeMenu.Settings,
                (app) =>
                {
                    HomeContext homeContext = app.Services.GetService<HomeContext>()
                        ?? throw new InvalidOperationException("HomeContext not found");

                    Button[] buttons = [];

                    Button back = new(
                        "Back",
                        app.Content.Load<SpriteFont>("Font/lookout"),
                        app.Content.Load<SpriteSheet>("SpriteSheet/menu_button"),
                        () => homeContext.ChangeMenu(HomeMenu.Overview));

                    buttons = [.. buttons, back];

                    return new Menu(buttons, 16);
                }
            }
        };

        public Menu CreateMenu(HomeMenu menu)
        {
            if (factories.TryGetValue(menu, out var factory))
            {
                return factory(app);
            }

            throw new InvalidOperationException("No such menu");
        }
    }
}