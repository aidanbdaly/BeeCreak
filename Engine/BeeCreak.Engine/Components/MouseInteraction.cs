using BeeCreak.Engine.Services;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Behaviours
{
    public class MouseInteraction(App app, Func<Rectangle> GetDomain) : GameComponent(app)
    {
        private InputService InputService => app.Services.GetService<InputService>();

        public event Action? OnClick;

        public event Action? OnHover;

        public override void Update(GameTime gameTime)
        {
            if (IsHovered)
            {
                if (true)
                {
                    OnClick?.Invoke();
                }
                else
                {
                    OnHover?.Invoke();
                }
            }
        }
        private bool IsHovered => GetDomain().Contains(InputService.GetMousePosition());
    }
}