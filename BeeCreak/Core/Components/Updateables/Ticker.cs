namespace BeeCreak.Core.Components
{
    public interface ITicker
    {
        int Ticks { get; }

        Action BindOnTick(Action<int> action);
    }

    public class Ticker(float intervalInSeconds) : Updateable, ITicker
    {
        private float elapsed = 0f;

        private event Action<int>? OnTick;

        public int Ticks { get; private set; } = 0;

        public Action BindOnTick(Action<int> action)
        {
            OnTick += action;
            return () => OnTick -= action;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
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