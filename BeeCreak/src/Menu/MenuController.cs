using BeeCreak.App;
using BeeCreak.Shared.Services.Dynamic;
using BeeCreak.Shared.UI;
using BeeCreak.UI;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Menu;

public class MenuScene : IScene
{
    private readonly ISound sound;

    private readonly MenuBackgroundRenderer menuBackgroundRenderer;

    private readonly ButtonGroupStore buttonGroupStore;

    private readonly ButtonGroupController buttonController;

    public MenuScene(ISound sound, ButtonGroupController buttonController, MenuBackgroundRenderer menuBackgroundRenderer)
    {
        this.sound = sound;

        this.buttonController = buttonController;
        this.menuBackgroundRenderer = menuBackgroundRenderer;
    }

    public void Initialize() {
        ActionRegistry.Register("ChangeMenuMain", () => ChangeMenu(ButtonGroupType.Menu_Main));
        ActionRegistry.Register("ChangeMenuOptions", () => ChangeMenu(ButtonGroupType.Menu_Options));
        ActionRegistry.Register("ChangeMenuLoad", () => ChangeMenu(ButtonGroupType.Menu_Load));
    }

    public void Enter()
    {
        sound.PlayMusic("garden-sanctuary");
    }

    public void Exit()
    {
    }

    private void ChangeMenu(ButtonGroupType menuType)
    {
        var buttons = buttonGroupStore.Get(menuType);

        buttonController.Load(buttons);
    }

    public void Update(GameTime gameTime)
    {
        buttonController.Update(gameTime);
    }

    public void Draw()
    {
        menuBackgroundRenderer.Draw();
        buttonController.Draw();
    }
}
