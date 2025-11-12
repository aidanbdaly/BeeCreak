using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components
{
    public class Text(string text, SpriteFont fontAsset, int? maxWidth = null) : Renderable
    {
        private readonly SpriteFont fontAsset = fontAsset;

        private readonly string renderedText = PrepareText(text, fontAsset, maxWidth);

        public override Rectangle GetBounds()
        {
            var size = fontAsset.MeasureString(renderedText) * Scale;

            return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)size.X,
                (int)size.Y
            );
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                fontAsset,
                renderedText,
                Position,
                Color,
                Rotation,
                Origin,
                Scale,
                SpriteEffects.None,
                0f);
        }

        private static string PrepareText(string text, SpriteFont font, int? maxWidth)
        {
            if (!maxWidth.HasValue || maxWidth <= 0)
            {
                return text;
            }

            var normalized = text.Replace("\r", "");
            var paragraphs = normalized.Split('\n');
            var lines = new List<string>();

            foreach (var paragraph in paragraphs)
            {
                var wrapped = WrapParagraph(paragraph, font, maxWidth.Value);

                if (wrapped.Count == 0)
                {
                    lines.Add(string.Empty);
                }
                else
                {
                    lines.AddRange(wrapped);
                }
            }

            return string.Join(Environment.NewLine, lines);
        }

        private static List<string> WrapParagraph(string paragraph, SpriteFont font, int maxWidth)
        {
            var lines = new List<string>();

            var words = paragraph.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0)
            {
                return lines;
            }

            var currentLine = string.Empty;

            foreach (var word in words)
            {
                var candidate = string.IsNullOrEmpty(currentLine) ? word : $"{currentLine} {word}";

                if (font.MeasureString(candidate).X <= maxWidth)
                {
                    currentLine = candidate;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentLine))
                    {
                        lines.Add(currentLine);
                    }

                    currentLine = word;
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                lines.Add(currentLine);
            }

            return lines;
        }
    }
}
