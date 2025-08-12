using BeeCreak.Shared.Services;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak
{
    public enum ButtonComponentState
    {
        Normal,
        Hovered,
    }

    public class ButtonComponent : SpriteComponent, IBehavior
    {
        private readonly string text;

        public readonly Action onClick;

        public ButtonComponent(string text, Action onClick, AssetHandle<SpriteSheet> buttonSpriteSheet) : base(buttonSpriteSheet)
        {
            this.text = text;
            this.onClick = onClick;
        }

        private MouseState LastMouseState { get; set; } = Mouse.GetState();

        private ButtonComponentState CurrentState { get; set; } = ButtonComponentState.Normal;

        public override Rectangle GetBounds()
        {
            return SourceRectangle;
        }

        public void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            var frame = GetBounds();

            var absoluteBounds = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                frame.Width * (int)Scale,
                frame.Height * (int)Scale
            );

            if (absoluteBounds.Contains(mouseState.Position))
            {
                if (CurrentState != ButtonComponentState.Hovered)
                {
                    CurrentState = ButtonComponentState.Hovered;

                }

                if (mouseState.LeftButton == ButtonState.Pressed && LastMouseState.LeftButton == ButtonState.Released)
                {
                    onClick?.Invoke();
                }
            }
            else
            {
                CurrentState = ButtonComponentState.Normal;
            }

            SetSprite(CurrentState.ToString());
            LastMouseState = mouseState;
        }
    };
}
