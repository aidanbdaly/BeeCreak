using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.UI;

public class Clock : Element
{
    public int Time { get; set; }
    public IToolCollection Tools { get; set; } = default!;

    public Clock(IToolCollection tools)
    {
        ScreenPosition = new Vector2(20, 20);
        Tools = tools;
    }

    public override void Update(GameTime gameTime)
    {
        Time = Tools.Dynamic.Time.Current;
    }

    public override void Draw()
    {
        var Sprite = Tools.Static.Sprite;

        var suffix = Time switch
        {
            0 => "AM",
            12 => "PM",
            _ => Time < 12 ? "AM" : "PM"
        };

        Sprite.Batch.Begin(
            sortMode: SpriteSortMode.Deferred,
            blendState: BlendState.AlphaBlend,
            samplerState: SamplerState.PointClamp
        );
        Sprite.Batch.DrawString(
            Sprite.GetFont("lookout"),
            $"Time: {Time} {suffix}",
            ScreenPosition,
            Color.White,
            0,
            Vector2.Zero,
            1.5f,
            SpriteEffects.None,
            0
        );
        Sprite.Batch.End();
    }
}
