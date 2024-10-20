using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Objects.Time;

public class Time : ITime
{
    public int Current { get; set; }

    public Time() { }

    public TimeDTO ToDTO()
    {
        return new TimeDTO() { Current = Current };
    }

    public void Update(GameTime gameTime)
    {
        var secondsInGameDay = 1800;

        Current = (int)(gameTime.TotalGameTime.TotalSeconds / secondsInGameDay * 24);
    }
}
