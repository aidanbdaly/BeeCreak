using BeeCreak.Core.State;

namespace BeeCreak.Core.Components
{
    public class Ticker(float intervalInSeconds) : Updateable
    {
        private readonly float intervalInSeconds = intervalInSeconds;

        private float elapsed = 0f;

        public State<int> ticks = new(0);

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (elapsed >= intervalInSeconds)
            {
                elapsed -= intervalInSeconds;
                ticks.Value += 1;
            }
        }
    }
}