using Microsoft.Xna.Framework;
using BeeCreak.Core.Input;

namespace BeeCreak.Core.Components
{
    public class MouseInteraction(
        Func<Rectangle> GetDomain,
        Func<Point> GetMousePosition,
        Func<PointerButtonMap, bool> IsMouseClicked
        ) : Updateable
    {
        private event Action? OnClick;

        private event Action? OnHover;

        private readonly Func<Point> GetMousePosition = GetMousePosition;

        private readonly Func<PointerButtonMap, bool> IsMouseClicked = IsMouseClicked;

        public Action BindOnClick(Action onClick)
        {
            OnClick += onClick;
            return () => OnClick -= onClick;
        }

        public Action BindOnHover(Action onHover)
        {
            OnHover += onHover;
            return () => OnHover -= onHover;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsHovered)
            {
                if (IsMouseClicked(PointerButtonMap.Left))
                {
                    OnClick?.Invoke();
                }
                else
                {
                    OnHover?.Invoke();
                }
            }
        }
        private bool IsHovered => GetDomain().Contains(GetMousePosition());
    }
}