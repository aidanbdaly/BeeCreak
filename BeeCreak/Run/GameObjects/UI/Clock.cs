using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects.Instances;

public class Clock : Element
{
    public int Time { get; set; }
    public IToolCollection Tools { get; set; } = default!;

    public Clock(IToolCollection tools)
    {
        ScreenPosition = new Vector2(10, 10);
        Tools = tools;
    }

    public override void Update(GameTime gameTime)
    {
        Time = Tools.Dynamic.Time.Current;
    }

    public override void Draw()
    {
        var Sprite = Tools.Static.Sprite;

        Sprite.Batch.Begin();
        Sprite.Batch.DrawString(
            Sprite.GetFont("lookout"),
            $"Time: {Time}",
            ScreenPosition,
            Color.White
        );
        Sprite.Batch.End();
    }
}
