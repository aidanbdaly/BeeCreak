namespace BeeCreak.Menu
{
    using System.Collections.Generic;
    using global::BeeCreak.Game;
    using global::BeeCreak.Tools;
    using global::BeeCreak.UI;
    using Microsoft.Xna.Framework;

    public class MenuManager : IDynamicRenderable
    {
        private readonly Dictionary<MenuMode, Menu> menus;
        private readonly Mode<MenuMode> menuMode;
        private readonly UISettings settings;

        public MenuManager(IToolCollection tools, Mode<RunMode> runMode, SaveManager saveManager)
        {
            this.menuMode = new Mode<MenuMode>(MenuMode.Main);

            settings = new UISettings();

            menus = new Dictionary<MenuMode, Menu>
            {
                { MenuMode.Main, new Main(tools, settings, menuMode) },
                { MenuMode.Load, new Load(tools, settings, menuMode, saveManager) },
                { MenuMode.Settings, new Settings(tools, settings, menuMode) },
            };
        }

        public void Update(GameTime gameTime)
        {
            menus[menuMode.Current].Update(gameTime);
        }

        public void Draw()
        {
            menus[menuMode.Current].Draw();
        }
    }
}
