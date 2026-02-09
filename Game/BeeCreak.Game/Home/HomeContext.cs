using BeeCreak.Engine;
using BeeCreak.Game.Home.Services;

namespace BeeCreak.Game.Home
{
    public class HomeContext(App app)
    {
        private readonly MenuFactory menuFactory = app.Services.GetService<MenuFactory>()
            ?? throw new InvalidOperationException("MenuFactory not found");

        public void Initialize()
        {
            ChangeMenu(HomeMenu.Overview);
        }

        public void ChangeMenu(HomeMenu targetMenu)
        {
           
        }
    }
}