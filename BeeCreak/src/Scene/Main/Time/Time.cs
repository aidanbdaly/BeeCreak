using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class Time : ITime
{
    public Time()
    {
    }

    public int Current { get; set; }

    public void Update(GameTime gameTime)
    {
        var secondsInGameDay = 1800;

        Current = (int)(gameTime.TotalGameTime.TotalSeconds / secondsInGameDay * 24);
    }
}