

using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MenuRenderer
{
    private readonly LayerManager sceneManager;

    public MenuRenderer(LayerManager sceneManager)
    {
        this.sceneManager = sceneManager;
    }

    public void Initialize()
    {
        sceneManager.AddLayer(new Layer()
        {
            Name = "Menu",
            Content = new List<BasicElement>()
            {
                new BasicElement()
                {
                    Name = "MenuBackground",
                    Texture = AssetManager.Get<Texture2D>("menu_background"),
                    Position = Vector2.Zero
                }
            },
            ZIndex = 0
        });
    }
}