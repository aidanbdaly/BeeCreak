namespace BeeCreak.Menu
{
    using System.Collections.Generic;
    using global::BeeCreak.Game;
    using global::BeeCreak.Tools;
    using global::BeeCreak.UI;
    using global::BeeCreak.UI.Components;
    using Microsoft.Xna.Framework;

    public class Load : Menu
    {
        private readonly Mode<MenuMode> menuMode;

        public Load(IToolCollection tools, UISettings uISettings, Mode<MenuMode> menuMode, SaveManager saveManager)
            : base(tools)
        {
            Texture = tools.Static.Sprite.GetTexture("menu-background");

            var saveFiles = saveManager.GetSaves();

            var buttons = new List<Element> { };

            foreach (var save in saveFiles)
            {
                buttons.Add(new Button(tools, uISettings, save, () => { }));
            }

            buttons.Add(new Button(tools, uISettings, "Back", () => menuMode.Switch(MenuMode.Main)));

            Children = new List<Element>
            {
                new ElementArray(
                    uISettings,
                    new Vector2(
                        tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                        tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2),
                    buttons,
                    16),
            };
        }
    }
}
