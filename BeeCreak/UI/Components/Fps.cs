using BeeCreak.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.UI.Instances;

public class Fps : Element
{
    private int FrameCount;
    private int FPS;
    private double ElapsedTime;
    private IToolCollection Tools;

    public Fps(IToolCollection tools)
    {
        Position = new Vector2(10, 40);
        Tools = tools;
    }

    public override void Update(GameTime gameTime)
    {
        ElapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
        FrameCount++;

        if (ElapsedTime >= 1)
        {
            FPS = FrameCount;
            FrameCount = 0;
            ElapsedTime = 0;
        }
    }

    public override void Draw()
    {
        var Sprite = Tools.Static.Sprite;

        Sprite.Batch.Begin();
        Sprite.Batch.DrawString(
            Sprite.GetFont("lookout"),
            $"FPS: {FPS}",
            Position,
            Color.White
        );
        Sprite.Batch.End();
    }
}
