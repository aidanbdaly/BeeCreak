using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Behaviours
{
    public class Timer(App app, float durationInSeconds) : GameComponent(app)
    {
        public event Action? OnCompletion;

        public event Action<float>? OnUpdate;

        private float elapsedTime = 0f;

        private bool enabled = true;

        public override void Update(GameTime gameTime)
        {
            if (enabled)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                OnUpdate?.Invoke(elapsedTime / durationInSeconds);

                if (elapsedTime >= durationInSeconds)
                {
                    OnCompletion?.Invoke();
                    enabled = false;
                }
            }
        }
    }
}