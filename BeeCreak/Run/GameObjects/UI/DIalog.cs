using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.UI;

public class Dialog : Element
{
    public Texture2D Texture { get; set; }
    private IToolCollection Tools { get; set; }
    private float Scale => 1.5f;

    public Dialog(IToolCollection tools)
    {
        Texture = tools.Static.Sprite.GetTexture("dialog");
        Tools = tools;

        ScreenPosition = new Vector2(
            Tools.Dynamic.Camera.ViewPortWidth / 2 - Texture.Width * Scale / 2,
            Tools.Dynamic.Camera.ViewPortHeight - Texture.Height * Scale - 10
        );
    }

    public override void Update(GameTime gameTime) { }

    public override void Draw()
    {
        var Sprite = Tools.Static.Sprite;

        Sprite.Batch.Begin(
            sortMode: SpriteSortMode.Deferred,
            blendState: BlendState.AlphaBlend,
            samplerState: SamplerState.PointClamp
        );
        Sprite.Batch.Draw(
            Texture,
            ScreenPosition,
            null,
            Color.White,
            0,
            Vector2.Zero,
            Scale,
            SpriteEffects.None,
            0
        );
        Sprite.Batch.End();
    }
}
