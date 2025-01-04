using BeeCreak.App;
using BeeCreak.Tools.Dynamic;
using BeeCreak.Tools.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Features.Menu;

public class MenuScene : IScene
{
    private readonly IApp app;

    private readonly ISprite sprite;

    private readonly ISound sound;

    private readonly MenuActions actions;

    public MenuScene(ISprite sprite, ISound sound, IApp app, MenuActions actions)
    {
        this.app = app;
        this.sprite = sprite;
        this.sound = sound;
        this.actions = actions;
    }

    private Texture2D Background { get; set; }

    public void Enter(string parameter = "")
    {
        sound.PlayMusic("garden-sanctuary");
        app.SetScene(this);
    }

    public void Exit(IScene nextScene = null)
    {
    }

    public void Load(Texture2D spriteSheet)
    {
        Background = spriteSheet;
    }

    public void Update(GameTime gameTime)
    {
        actions.Update(gameTime);
    }

    public void Draw()
    {
        sprite.Batch.Begin(
         SpriteSortMode.Deferred,
         BlendState.AlphaBlend,
         SamplerState.PointClamp);

        var position = new Vector2(
            sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
            sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height / 2);

        var backgroundScale =
            (float)sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width
            / Background.Width;

        sprite.Batch.Draw(
            Background,
            position,
            null,
            Color.White,
            0,
            new Vector2(Background.Width / 2, Background.Height / 2),
            backgroundScale,
            SpriteEffects.None,
            0);

        sprite.DrawString(
            "Bee Creak",
            new Vector2(
                    sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2,
                    sprite.GraphicsDevice.Adapter.CurrentDisplayMode.Height * 1 / 4),
            Color.Black,
            0f,
            true
        );

        actions.Draw();

        sprite.Batch.End();
    }
}
