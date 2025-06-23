using Microsoft.Xna.Framework;

public class LoadMenuComponent : ComponentCollection, ILayoutable
{
    public LoadMenuComponent(Game context, BeeCreak.BeeCreak game)
    {
        Scale = 3f;

        var saveFiles = Directory.GetFiles("src/Saves", "*");

        foreach (var saveFile in saveFiles)
        {
            var fileName = Path.GetFileNameWithoutExtension(saveFile);
            Components.Add(new ButtonComponent(fileName, () =>
            {
                context.SaveId = fileName;
                game.ChangeScene<GameScene>();
            }));
        }
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