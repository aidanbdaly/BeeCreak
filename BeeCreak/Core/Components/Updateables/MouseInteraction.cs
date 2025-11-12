using Microsoft.Xna.Framework;
using BeeCreak.Core.Input;

namespace BeeCreak.Core.Components
{
    public class MouseInteraction(
        Renderable Ref,
        Action OnClick,
        Action OnHover,
        Func<Point> GetMousePosition,
        Func<PointerButtonMap, bool> IsMouseClicked
        ) : Updateable
    {
        private readonly Action OnClick = OnClick;

        private readonly Action OnHover = OnHover;

        private readonly Func<Point> GetMousePosition = GetMousePosition;

        private readonly Func<PointerButtonMap, bool> IsMouseClicked = IsMouseClicked;

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
        private bool IsHovered => Ref.GetBounds().Contains(GetMousePosition());
    }
}