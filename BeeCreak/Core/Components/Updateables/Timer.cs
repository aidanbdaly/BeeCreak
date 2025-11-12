namespace BeeCreak.Core.Components
{
    public class Ticker(float intervalInSeconds) : Updateable
    {
        private readonly float intervalInSeconds = intervalInSeconds;

        private float elapsedTime = 0f;

        public event Action? OnTick;

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsedTime >= intervalInSeconds)
            {
                OnTick?.Invoke();
                elapsedTime -= intervalInSeconds;
            }
        }
    }
}