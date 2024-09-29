using BeeCreak.Run.Game.Objects.Time;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.Game.UI.Instances;

public class Clock : Element
{
    private ITime Time { get; set; }
    private IToolCollection Tools { get; set; }

    public Clock(IToolCollection tools, ITime time)
    {
        ScreenPosition = new Vector2(20, 20);
        Time = time;
        Tools = tools;
    }

    public override void Update(GameTime gameTime)
    {
        Time.Update(gameTime);
    }

    public override void Draw()
    {
        var Sprite = Tools.Static.Sprite;

        var suffix = Time.Current switch
        {
            0 => "AM",
            12 => "PM",
            _ => Time.Current < 12 ? "AM" : "PM"
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
