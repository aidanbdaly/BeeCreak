using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Scene.Main;

public class Clock : IClock
{
    private readonly ISpriteController spriteController;

    public Clock(ISpriteController spriteController)
    {
        this.spriteController = spriteController;

        Position = new Vector2(20, 20);
    }

    public Vector2 Position { get; set; }

    private ITime Time { get; set; }

    public void SetTime(ITime time)
    {
        Time = time;
    }

    public void Update(GameTime gameTime)
    {
        Time.Update(gameTime);
    }

    public void Draw()
    {
        var suffix = Time.Current switch
        {
            0 => "AM",
            12 => "PM",
            _ => Time.Current < 12 ? "AM" : "PM"
        };

        spriteController.Batch.Begin(
            sortMode: SpriteSortMode.Deferred,
            blendState: BlendState.AlphaBlend,
            samplerState: SamplerState.PointClamp);
        spriteController.DrawString(
            $"Time: {Time} {suffix}",
            Position,
            Color.White,
            0,
            true,
            1.5f,
            SpriteEffects.None,
            0);
        spriteController.Batch.End();
    }
}