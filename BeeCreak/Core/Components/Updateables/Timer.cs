namespace BeeCreak.Core.Components
{
    public class Timer(float durationInSeconds) : Updateable
    {
        private readonly float durationInSeconds = durationInSeconds;

        public event Action? OnCompletion;

        public event Action<float>? OnUpdate;

        private float elapsedTime = 0f;

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            OnUpdate?.Invoke(elapsedTime / durationInSeconds);

            if (elapsedTime >= durationInSeconds)
            {
                OnCompletion?.Invoke();
                IsEnabled = false;
            }
        }
    }
}