namespace BeeCreak.UI.Components
{
    using System;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Button : Element
    {
        private readonly IUISettings settings;
        private readonly string text;
        private readonly Action action;
        private readonly ISprite sprite;
        private MouseState previousState;

        public Button(ISprite sprite, IUISettings settings, string text, Action action)
        {
            this.settings = settings;
            this.text = text;
            this.action = action;
            this.sprite = sprite;

            Texture = sprite.GetTexture("button");
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            var mousePosition = new Vector2(mouseState.X, mouseState.Y);

            var scale = settings.Scale;

            var hovered = mousePosition.X > Position.X - (Texture.Width / 2 * scale)
                && mousePosition.X < Position.X + (Texture.Width / 2 * scale)
                && mousePosition.Y > Position.Y - (Texture.Height / 2 * scale)
                && mousePosition.Y < Position.Y + (Texture.Height / 2 * scale);

            if (
                hovered
                && mouseState.LeftButton == ButtonState.Released
                && previousState.LeftButton == ButtonState.Pressed)
            {
                action();
            }

            previousState = mouseState;

            Texture = sprite.GetTexture(hovered ? "button-hovered" : "button");
        }

        public override void Draw()
        {
            sprite.Batch.Draw(Texture, Position, null, Color.White, 0, new Vector2(Texture.Width / 2, Texture.Height / 2), settings.Scale, SpriteEffects.None, 0);

            sprite.Batch.DrawString(
                sprite.GetFont("lookout"),
                text,
                Position,
                Color.Black,
                0,
                new Vector2(
                    sprite.GetFont("lookout").MeasureString(text).X / 2,
                    sprite.GetFont("lookout").MeasureString(text).Y / 2),
                settings.Scale,
                SpriteEffects.None,
                0);
        }
    }
}