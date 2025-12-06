using BeeCreak.Engine.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Graphics
{
    public class TextComponent(App app, SpriteFont font) : Sprite(app)
    {
        public State<string> Text { get; set; } = new(string.Empty);

        public State<Vector2> Position { get; set; } = new(Vector2.Zero);

        public State<Color> Color { get; set; } = new(default);

        public State<float> Opacity { get; set; } = new(1f);

        public State<float> Rotation { get; set; } = new(0f);

        public State<Vector2> Origin { get; set; } = new(default);

        public State<Vector2> Scale { get; set; } = new(Vector2.One);

        public State<SpriteEffects> Effects { get; set; } = new(default);

        public State<float> LayerDepth { get; set; } = new(0f);

        public override Rectangle BoundingBox =>
            new(Position.Value.ToPoint(), (font.MeasureString(Text.Value) * Scale.Value).ToPoint());

        public override void Draw(GameTime gameTime)
        {
            app.SpriteBatch.DrawString(
                font,
                Text.Value,
                Position.Value,
                Color.Value,
                Rotation.Value,
                Origin.Value,
                Scale.Value,
                Effects.Value,
                LayerDepth.Value);
        }
    }

    public class TextFormatter(int? maxWidth = null)
    {
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
