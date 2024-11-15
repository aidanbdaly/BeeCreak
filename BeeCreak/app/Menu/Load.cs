namespace BeeCreak.Menu
{
    using System.Collections.Generic;
    using global::BeeCreak.Game;
    using global::BeeCreak.Tools.Static;
    using global::BeeCreak.UI;
    using global::BeeCreak.UI.Components;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Load : Menu
    {
        public Load(
            ISprite sprite, IUISettings uISettings, IAppRouter appRouter, ISaveManager saveManager)
            : base(sprite)
        {
            Texture = sprite.GetTexture("menu-background");

            var saveFiles = saveManager.GetSaves();

            var buttons = new List<Element> { };

            foreach (var save in saveFiles)
            {
                buttons.Add(new Button(sprite, uISettings, save, () => { }));
            }

            buttons.Add(new Button(sprite, uISettings, "Back", () => appRouter.Navigate("mainMenu")));

            Children = new List<Element>
            {
                new ElementArray(
                    uISettings,
                    new Vector2(
                        sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                        sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height * 2 / 3),
                    buttons,
                    16),
            };
        }
    }
}
