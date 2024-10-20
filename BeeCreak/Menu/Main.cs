namespace BeeCreak.Menu
{
    using System.Collections.Generic;
    using global::BeeCreak.Events;
    using global::BeeCreak.Tools;
    using global::BeeCreak.UI;
    using global::BeeCreak.UI.Components;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Xna.Framework;

    public class Main : Menu
    {
        private readonly UISettings settings;
        private readonly Mode<MenuMode> menuMode;

        public Main(IToolCollection tools, UISettings uISettings, Mode<MenuMode> menuModeManager)
            : base(tools)
        {
            settings = uISettings;
            Texture = tools.Static.Sprite.GetTexture("menu-background");
            menuMode = menuModeManager;

            var buttons = new List<Element>
            {
                new Button(tools, settings, "New Game", () => tools.Static.Events.Dispatch(new NewGameEvent())),
                new Button(tools, settings, "Load Game", () => menuMode.Switch(MenuMode.Load)),
                new Button(tools, settings, "Settings", () => menuMode.Switch(MenuMode.Settings)),
                new Button(tools, settings, "Exit Game", () => { }),
            };

            Children = new List<Element>
            {
                new ElementArray(
                    settings,
                    new Vector2(
                        tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                        tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2),
                    buttons,
                    16),
            };
        }
    }
}
