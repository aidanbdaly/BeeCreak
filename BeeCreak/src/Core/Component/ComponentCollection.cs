using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class ComponentCollection : IComponent, IBehavior
{
    protected List<IComponent> Components = [];

    private List<IComponent> _componentsToRemove = [];

    private List<IComponent> _componentsToAdd = [];

    private bool _isUpdating;

    public Vector2 Position { get; set; }

    public float Scale { get; set; } = 1f;

    public Texture2D? Texture { get; set; }

    public virtual void LoadContent(AssetManager assetManager)
    {
        foreach (var component in Components)
        {
            component.LoadContent(assetManager);
        }
    }

    public virtual void UnloadContent(AssetManager assetManager)
    {
        foreach (var component in Components)
        {
            component.UnloadContent(assetManager);
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach (var component in Components)
        {
            if (component is IBehavior updatable)
            {
                updatable.Update(gameTime);
            }
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in Components)
        {
            component.Draw(spriteBatch);
        }
    }
}