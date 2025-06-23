
using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Scene : IScene
{
    protected readonly List<IComponent> components = new();

    protected readonly List<IBehavior> behaviors = new();

    protected readonly List<ILoadable> managers = new();

    virtual public void LoadContent(AssetManager assetManager)
    {
        foreach (var component in components)
        {
            component.LoadContent(assetManager);
        }

        foreach (var manager in managers)
        {
            manager.LoadContent(assetManager);
        }
    }

    virtual public void UnloadContent(AssetManager assetManager)
    {
        foreach (var component in components)
        {
            component.UnloadContent(assetManager);
        }

        foreach (var behavior in behaviors)
        {
            if (behavior is ILoadable loadable)
            {
                loadable.UnloadContent(assetManager);
            }
        }
    }

    virtual public void PerformLayout(GameWindow gameWindow)
    {

    }

    virtual public void Update(GameTime gameTime)
    {
        foreach (var updateable in behaviors)
        {
            updateable.Update(gameTime);
        }

        foreach (var component in components)
        {
            if (component is IBehavior updateable)
            {
                updateable.Update(gameTime);
            }
        }
    }

    virtual public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }
    }
}