using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;

public class MainMenuComponent : ComponentCollection, ILayoutable
{
    public MainMenuComponent(Game context, BeeCreak.BeeCreak game, LoadMenuComponent loadMenu)
    {
        Scale = 3f;

        Components.Add(new ButtonComponent("New Game", () =>
        {
            context.SaveId = DateAndTime.Now.ToString("yyyyMMddHHmmss");
            game.ChangeScene<GameScene>();
        }));

        Components.Add(new ButtonComponent("Load Game", () => 
        {

        }));

        Components.Add(new ButtonComponent("Options", () => { }));

        Components.Add(new ButtonComponent("Exit", game.Exit));
    }

    public void PerformLayout(GameWindow window)
    {
        for (int i = 0; i < Components.Count; i++)
        {
            if (Components[i] is ButtonComponent button)
            {
                button.Scale = Scale;
                button.Position = new Vector2(
                    (int)(window.ClientBounds.Width - button.Texture.Width * Scale) / 2,
                    (int)(window.ClientBounds.Height - button.Texture.Height * Scale) / 2 + button.Texture.Height * Scale * i + (10 * i)
                );
            }
        }
    }
}