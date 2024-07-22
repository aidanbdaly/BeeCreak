namespace BeeCreak.Run;

using Microsoft.Xna.Framework;
public class TimeController : IDynamicObject
{
    public int Time { get; set; }
    public TimeController() {}
    public void Update(GameTime gameTime)
    {
        var secondsInGameDay = 1800;

        Time = (int)(gameTime.TotalGameTime.TotalSeconds / secondsInGameDay * 100);
    }
}