using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public class DirectionalInputComponent : GameComponent
    {
        private readonly InputService input;

        public DirectionalInputComponent(App app) : base(app)
        {
            input = app.Services.GetService<InputService>();

            app.Services.AddService<DirectionalInputComponent>(this);
        }

        public event EventHandler<ButtonMap>? OnInput;

        public override void Update(GameTime gameTime)
        {
            if (input.IsButtonDown(ButtonMap.Up))
            {
                OnInput?.Invoke(this, ButtonMap.Up);
            }

            if (input.IsButtonDown(ButtonMap.Down))
            {
                OnInput?.Invoke(this, ButtonMap.Down);
            }

            if (input.IsButtonDown(ButtonMap.Left))
            {
                OnInput?.Invoke(this, ButtonMap.Left);
            }

            if (input.IsButtonDown(ButtonMap.Right))
            {
                OnInput?.Invoke(this, ButtonMap.Right);
            }
        }
    }
}