using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MenuScene : IScene
{
    private readonly List<IComponent> components = new();

    public MenuScene()
    {
        components.Add(new FullscreenComponent("Textures/Backgrounds/MenuBackground"));
        components.Add(new MenuOptionsComponent());
    }

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

    public void PerformLayout(GameWindow window)
    {
        foreach (var component in components)
        {
            if (component is ILayoutable layoutable)
            {
                layoutable.UpdateLayout(window);
            }
        }
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
        spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                sortMode: SpriteSortMode.Immediate
            );

        foreach (var component in components)
        {
            component.Draw(spriteBatch);
        }

        spriteBatch.End();
    }
}