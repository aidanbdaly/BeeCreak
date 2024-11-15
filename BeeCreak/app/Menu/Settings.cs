namespace BeeCreak.Menu
{
    using System.Collections.Generic;
    using global::BeeCreak.Tools.Static;
    using global::BeeCreak.UI;
    using global::BeeCreak.UI.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Settings : Menu
    {
        public Settings(ISprite sprite, IUISettings uISettings, IAppRouter appRouter)
        : base(sprite)
        {
            Texture = sprite.GetTexture("menu-background");

            Children = new List<Element>
            {
                    new ElementArray(
                        uISettings,
                        new Vector2(
                            sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                            sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2),
                        new List<Element> { new Button(sprite, uISettings, "Back", () => appRouter.Navigate("mainMenu")) },
                        16),
            };
        }
    }
}
