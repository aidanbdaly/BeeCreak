namespace BeeCreak.Game.Time
{
    using Microsoft.Xna.Framework;

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
}