using BeeCreak.Shared.Services;
using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Scene.Menu;

public class MenuBackgroundRenderer
{
    private readonly SpriteController spriteController;

    private const string SpriteSheetName = "menu-background";

    public MenuBackgroundRenderer(SpriteController spriteController)
    {
        this.spriteController = spriteController;
    }

    public void Draw()
    {
        spriteController.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp);

        spriteController.DrawRelative(
            AssetManager.Get<Texture2D>(SpriteSheetName),
            new Vector2(1 / 2, 1 / 2),
            ScaleType.FitViewportWidth,
            null
            );

        spriteController.DrawStringRelative(
            "Bee Creak",
            new Vector2(
                   1 / 2,
                   1 / 4),
            Color.Black,
            5
        );

        spriteController.End();
    }
}
