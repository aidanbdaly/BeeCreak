using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.Game.UI.Instances;

public class Dialog : Element
{
    private Texture2D Texture { get; set; }
    private IToolCollection Tools { get; set; }
    private float Scale => 1.5f;

    public Dialog(IToolCollection tools)
    {
        Texture = tools.Static.Sprite.GetTexture("dialog");
        Tools = tools;

        ScreenPosition = new Vector2(
            tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width / 2
                - Texture.Width * Scale / 2,
            tools.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height
                - Texture.Height * Scale
                - 10
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
