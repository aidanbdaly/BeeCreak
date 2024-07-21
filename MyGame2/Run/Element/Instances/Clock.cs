namespace BeeCreak.Run;

using Microsoft.Xna.Framework;

public class Clock : Element
{
    public int Time { get; set; }
    public IContext Context { get; set; } = default!;

    public Clock(IContext context)
    {
        ScreenPosition = new Vector2(10, 10);
        Context = context;
    }

    public override void Update(GameTime gameTime)
    {
        Time = Context.Dynamic.TimeController.Time;
    }

    public override void Draw()
    {
        var SpriteController = Context.Static.SpriteController;

        SpriteController.Batch.Begin();
        SpriteController.Batch.DrawString(
            SpriteController.GetFont("lookout"),
            Time.ToString(),
            ScreenPosition,
            Color.White
        );
        SpriteController.Batch.End();
    }
}
