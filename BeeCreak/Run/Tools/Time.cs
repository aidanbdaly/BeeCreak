using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Tools;

public class Time : IDynamicObject
{
    public int Current { get; set; }

    public Time() { }

    public void Update(GameTime gameTime)
    {
        var secondsInGameDay = 1800;

        Current = (int)(gameTime.TotalGameTime.TotalSeconds / secondsInGameDay * 24);
    }
}
