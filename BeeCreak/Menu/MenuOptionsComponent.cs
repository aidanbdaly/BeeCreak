using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MenuOptionsComponent : IComponent, IUpdateable, ILayoutable
{
    private readonly List<IComponent> components = new();

    public MenuOptionsComponent()
    {
        components.Add(new ButtonComponent("New Game", () => { }));
        components.Add(new ButtonComponent("Load Game", () => { }));
        components.Add(new ButtonComponent("Options", () => { }));
        components.Add(new ButtonComponent("Exit", () => { }));
    }

    public Vector2 Position { get; set; } = Vector2.Zero;

    public float Scale { get; set; } = 1f;

    public Texture2D? Texture { get; set; }

    public void LoadContent(AssetManager assetManager)
    {
        foreach (var component in components)
        {
            component.LoadContent(assetManager);
        }
    }

    public void UnloadContent()
    {
        foreach (var component in components)
        {
            component.UnloadContent();
        }
    }

    public void UpdateLayout(GameWindow window)
    {
        // Example of how to use the component positioner
        // Uncomment and implement the methods in ComponentPositioner if needed

        // componentPositioner.VerticalAlign(components, 0.5f);
        // componentPositioner.HorizontalAlign(components, 0.5f);
        // componentPositioner.DistributeVertically(components, 0.05f);
    }

    public void Update(GameTime gameTime)
    {
        foreach (var component in components)
        {
            if (component is IUpdateable updateable)
            {
                updateable.Update(gameTime);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }
}