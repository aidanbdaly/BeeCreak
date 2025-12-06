using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Behaviours
{
    public interface ITicker
    {
        int Ticks { get; }
    }

    public class Ticker(App app, float intervalInSeconds) : GameComponent(app), ITicker
    {
        private float elapsed = 0f;

        public event Action<int>? OnTick;

        public int Ticks { get; private set; } = 0;

        public override void Update(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsed >= intervalInSeconds)
            {
                elapsed -= intervalInSeconds;
                Ticks++;
                OnTick?.Invoke(Ticks);
            }
        }
    }
}