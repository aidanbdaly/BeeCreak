using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MenuScene : Scene
{
    public MenuScene(MainMenuComponent mainMenu)
    {
        components.Add(new FullscreenComponent("Image/menu-background"));
        components.Add(mainMenu);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);

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