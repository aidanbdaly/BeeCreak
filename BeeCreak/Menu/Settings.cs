namespace BeeCreak.Menu
{
    using System.Collections.Generic;
    using global::BeeCreak.Game.UI;
    using global::BeeCreak.Tools;
    using global::BeeCreak.UI;
    using global::BeeCreak.UI.Components;
    using Microsoft.Xna.Framework;

    public class Settings : Menu
    {
        public Settings(IToolCollection tools, UISettings uISettings, Mode<MenuMode> menuMode)
        : base(tools)
        {
            Texture = tools.Static.Sprite.GetTexture("menu-background");

            Children = new List<Element>
            {
                    new ElementArray(
                        uISettings,
                        new Vector2(
                            tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                            tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2),
                        new List<Element> { new Button(tools, uISettings, "Back", () => menuMode.Switch(MenuMode.Main)), },
                        16),
            };
        }
    }
}
