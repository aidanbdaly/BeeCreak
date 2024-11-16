namespace BeeCreak.Menu
{
    using System.Collections.Generic;
    using global::BeeCreak.Events;
    using global::BeeCreak.Tools.Dynamic;
    using global::BeeCreak.Tools.Static;
    using global::BeeCreak.UI;
    using global::BeeCreak.UI.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Main : Menu
    {
        private readonly IUISettings settings;

        public Main(ISprite sprite, ISound sound, IEventManager events, IUISettings uISettings, IAppRouter appRouter)
            : base(sprite)
        {
            settings = uISettings;
            Texture = sprite.GetTexture("menu-background");

            sound.PlayMusic("garden-sanctuary");

            var buttons = new List<Element>
            {
                new Button(sprite, settings, "New Game", () => events.Dispatch(new NewGameEvent())),
                new Button(sprite, settings, "Load Game", () => appRouter.Navigate("mainMenu/load")),
                new Button(sprite, settings, "Settings", () => appRouter.Navigate("mainMenu/settings")),
                new Button(sprite, settings, "Exit Game", () => { }),
            };

            Children = new List<Element>
            {
                new ElementArray(
                    settings,
                    new Vector2(
                        sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                        sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height * 2 / 3),
                    buttons,
                    16),
            };
        }
    }
}
