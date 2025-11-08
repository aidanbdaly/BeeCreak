using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BeeCreak.Core.Models;
using BeeCreak.Core.Input;

namespace BeeCreak.Core.Components
{
    public enum ButtonComponentState
    {
        Normal,
        Hovered,
    } 

    public sealed class Button : Renderable, IUpdateable
    {
        private readonly Sprite sprite;

        private readonly Text text;

        private readonly Action OnClick;

        private readonly Func<Point> GetMousePosition;

        private readonly Func<PointerButtonMap, bool> IsMouseClicked;

        public Button(
            string Text,
            Asset<SpriteSheet> SpriteSheet,
            Asset<SpriteFont> Font,
            Action OnClick,
            Func<Point> GetMousePosition,
            Func<PointerButtonMap, bool> IsMouseClicked
        )
        {
            sprite = new Sprite(SpriteSheet, CurrentState.ToString());
            text = new Text(Text, Font);

            this.OnClick = OnClick;
            this.GetMousePosition = GetMousePosition;
            this.IsMouseClicked = IsMouseClicked;
        }

        private ButtonComponentState CurrentState { get; set; } = ButtonComponentState.Normal;

        public override void Initialize()
        {
            sprite.Origin = sprite.GetBounds().Size.ToVector2() / 2;
            text.Origin = text.GetBounds().Size.ToVector2() / 2;
            text.Position = new Vector2(0, -4);
        }

        public void Update(GameTime gameTime)
        {
            var newState = CurrentState;

            if (IsHovered)
            {
                if (IsMouseClicked(PointerButtonMap.Left))
                {
                    OnClick?.Invoke();
                }

                newState = ButtonComponentState.Hovered;
                text.Position = Vector2.Zero;
            }
            else
            {
                newState = ButtonComponentState.Normal;
                text.Position = new Vector2(0, -4);
            }

            if (newState != CurrentState)
            {
                sprite.SetSprite(newState.ToString());
                CurrentState = newState;
            }
        }

        public override Rectangle GetBounds()
        {
            return sprite.GetBounds();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
            text.Draw(spriteBatch);
        }

        public override void Dispose()
        {
            sprite.Dispose();
            text.Dispose();
        }

        private bool IsHovered => GetBounds().Contains(GetMousePosition());
    };
}
