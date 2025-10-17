using Microsoft.Xna.Framework;
using BeeCreak.Engine.Input;
using BeeCreak.Engine.Assets;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public enum ButtonComponentState
    {
        Normal,
        Hovered,
    }

    public sealed class ButtonComponent : Component
    {
        private readonly SpriteComponent spriteComponent;

        private readonly TextComponent textComponent;

        private readonly Action OnClick;

        private readonly Func<Point> GetMousePosition;

        public ButtonComponent(
            string Text,
            AssetHandle<SpriteSheet> SpriteSheet,
            AssetHandle<SpriteFont> Font,
            Action OnClick,
            Func<Point> GetMousePosition
        )
        {
            spriteComponent = new SpriteComponent(SpriteSheet, CurrentState.ToString());
            textComponent = new TextComponent(Text, Font);

            this.OnClick = OnClick;
            this.GetMousePosition = GetMousePosition;

            spriteComponent.Origin = spriteComponent.GetBounds().Size.ToVector2() / 2;

            textComponent.Origin = textComponent.GetBounds().Size.ToVector2() / 2;

            textComponent.UpdateLocalTransform(new Vector2(0, -4), 0, 1);
        }

        private ButtonComponentState CurrentState { get; set; } = ButtonComponentState.Normal;

        public override void Update(GameTime gameTime)
        {
            var newState = CurrentState;

            if (IsHovered)
            {
                if (InputManager.IsMouseClicked())
                {
                    OnClick?.Invoke();
                }

                newState = ButtonComponentState.Hovered;
                textComponent.UpdateLocalTransform(new Vector2(0, 0), 0, 1);
            }
            else
            {
                newState = ButtonComponentState.Normal;
                textComponent.UpdateLocalTransform(new Vector2(0, -4), 0, 1);
            }

            if (newState != CurrentState)
            {
                spriteComponent.SetSprite(newState.ToString());
                CurrentState = newState;
            }

            base.Update(gameTime);
        }

        public override Rectangle GetBounds()
        {
            return Rectangle.Union(spriteComponent.GetBounds(), textComponent.GetBounds());
        }

        public override void Dispose()
        {
            spriteComponent.Dispose();
            textComponent.Dispose();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteComponent.Draw(spriteBatch);
            textComponent.Draw(spriteBatch);
        }

        private bool IsHovered => spriteComponent.GetBounds().Contains(GetMousePosition());
    };
}
