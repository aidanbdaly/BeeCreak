using System.Text;
using BeeCreak.Shared.Services;
using Microsoft.Extensions.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class TextComponent : TextureComponent, ILoadable, IResponsive
    {
        private readonly string text;

        private readonly int maxWidth;

        public TextComponent(string text, int maxWidth)
        {
            this.text = text;
            this.maxWidth = maxWidth;
        }

        private SpriteFont Font { get; set; }

        private List<string> Lines { get; set; } = [];

        public override Rectangle GetBounds()
        {
            return new Rectangle(0, 0, maxWidth * (int)Scale, (int)Lines.Select<string, float>((line) => Font.MeasureString(line).Y * Scale).Sum());
        }

        public void Layout(GameWindow window)
        {
            var currentLine = new StringBuilder();

            foreach (var word in text.Split(" "))
            {
                if (Font.MeasureString(new StringBuilder().Append(currentLine).Append($"{word} ").ToString()).X > maxWidth)
                {
                    Lines.Add(currentLine.ToString());
                    currentLine.Clear();
                }

                currentLine.Append($"{word} ");
            }

            if (currentLine.Length > 0)
            {
                Lines.Add(currentLine.ToString());
            }
        }

        public void LoadContent(AssetManager assetManager)
        {
            Font = assetManager.Load<SpriteFont>("Font/lookout");
        }

        public void UnloadContent(AssetManager assetManager)
        {
            // No specific unload logic for now
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp,
                blendState: BlendState.AlphaBlend,
                sortMode: SpriteSortMode.Deferred
            );

            for (int i = 0; i < Lines.Count; i++)
            {
                spriteBatch.DrawString(Font, Lines[i], new Vector2(Position.X, Position.Y + i * Font.LineSpacing * Scale), Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
            }

            spriteBatch.End();
        }
    }
}