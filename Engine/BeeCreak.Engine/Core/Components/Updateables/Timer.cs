namespace BeeCreak.Core.Components
{
    public class Timer(float durationInSeconds) : Updateable
    {
        private event Action? OnCompletion;

        private event Action<float>? OnUpdate;

        private float elapsedTime = 0f;

        private bool enabled = true;

        public Action BindOnCompletion(Action onCompletion)
        {
            OnCompletion += onCompletion;
            return () => OnCompletion -= onCompletion;
        }

        public Action BindOnUpdate(Action<float> onUpdate)
        {
            OnUpdate += onUpdate;
            return () => OnUpdate -= onUpdate;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
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