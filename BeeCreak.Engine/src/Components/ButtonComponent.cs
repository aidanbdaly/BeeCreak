using BeeCreak.Engine.Assets;
using Microsoft.Xna.Framework;
using BeeCreak.Engine.Input;
using BeeCreak.Engine.Components;
using BeeCreak.Engine.Core;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public enum ButtonComponentState
    {
        Normal,
        Hovered,
    }

    public record ButtonProps(AssetHandle<SpriteSheet> SpriteSheet, AssetHandle<SpriteFont> Font, string Text, Action OnClick, Func<Point> GetMousePosition);

    public class ButtonComponent : ComponentCollection<Component>
    {
        private readonly ButtonProps props;

        private readonly SpriteComponent spriteComponent;

        private readonly TextComponent textComponent;

        public ButtonComponent(ButtonProps props)
        {
            this.props = props;

            spriteComponent = new SpriteComponent(props.SpriteSheet);
            textComponent = new TextComponent(props.Text, props.Font) { Color = Color.Black };

            spriteComponent.SetSprite(ButtonComponentState.Normal.ToString());

            spriteComponent.Origin = spriteComponent.GetBounds().Size.ToVector2() / 2;

            textComponent.Origin = textComponent.GetBounds().Size.ToVector2() / 2;

            textComponent.UpdateLocalTransform(
                    new Vector2(0, -4), // Slight offset to look more centered
                    0,
                    1
            );

            components.Add(spriteComponent);
            components.Add(textComponent);
        }

        private ButtonComponentState CurrentState { get; set; } = ButtonComponentState.Normal;

        public override void Update(GameTime gameTime)
        {
            var newState = CurrentState;

            if (IsHovered)
            {
                if (InputManager.IsMouseClicked())
                {
                    props.OnClick?.Invoke();
                }

                newState = ButtonComponentState.Hovered;
                textComponent.UpdateLocalTransform(
                    new Vector2(0, 0), // Slight offset to look more centered
                    0,
                    1
                );
            }
            else
            {
                newState = ButtonComponentState.Normal;
                textComponent.UpdateLocalTransform(
                    new Vector2(0, -4), // Slight offset to look more centered
                    0,
                    1
                );
            }

            if (newState != CurrentState)
            {
                spriteComponent.SetSprite(newState.ToString());
                CurrentState = newState;
            }

            base.Update(gameTime);
        }

        private bool IsHovered => spriteComponent.GetBounds().Contains(props.GetMousePosition());
    };
}
