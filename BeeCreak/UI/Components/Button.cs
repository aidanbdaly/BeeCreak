namespace BeeCreak.UI.Components
{
    using System;
    using global::BeeCreak.Game.UI;
    using global::BeeCreak.Tools;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Button : Element
    {
        private readonly UISettings settings;
        private readonly string text;
        private readonly Action action;
        private readonly IToolCollection tools;
        private MouseState previousState;

        public Button(IToolCollection tools, UISettings settings, string text, Vector2 position, Action action)
        {
            this.tools = tools;
            this.settings = settings;
            this.text = text;
            this.action = action;

            Position = position;

            Texture = tools.Static.Sprite.GetTexture("button");
        }

        public Button(IToolCollection tools, UISettings settings, string text, Action action)
        {
            this.tools = tools;
            this.settings = settings;
            this.text = text;
            this.action = action;

            Texture = tools.Static.Sprite.GetTexture("button");
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

            Texture = tools.Static.Sprite.GetTexture(hovered ? "button-hovered" : "button");
        }

        public override void Draw()
        {
            tools.Static.Sprite.Batch.Draw(Texture, Position, null, Color.White, 0, new Vector2(Texture.Width / 2, Texture.Height / 2), settings.Scale, SpriteEffects.None, 0);

            tools.Static.Sprite.Batch.DrawString(
                tools.Static.Sprite.GetFont("lookout"),
                text,
                Position,
                Color.Black,
                0,
                new Vector2(
                    tools.Static.Sprite.GetFont("lookout").MeasureString(text).X / 2,
                    tools.Static.Sprite.GetFont("lookout").MeasureString(text).Y / 2),
                settings.Scale,
                SpriteEffects.None,
                0);
        }
    }
}