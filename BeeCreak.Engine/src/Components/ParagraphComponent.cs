using System.Text;
using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Components;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class ParagraphComponent : ComponentArray<TextComponent>
    {
        public ParagraphComponent(string text, int maxWidth, AssetHandle<SpriteFont> fontAsset) : base(0)
        {
            var currentLine = new StringBuilder();
            var lines = new List<TextComponent>();

            foreach (var word in text.Split(" "))
            {
                if (fontAsset.Asset.MeasureString(new StringBuilder().Append(currentLine).Append($"{word} ").ToString()).X > maxWidth)
                {
                    var textComponent = new TextComponent(currentLine.ToString(), fontAsset);

                    textComponent.Origin = textComponent.GetBounds().Size.ToVector2() / 2;

                    lines.Add(textComponent);

                    currentLine.Clear();
                }

                currentLine.Append($"{word} ");
            }

            if (currentLine.Length > 0)
            {
                var textComponent = new TextComponent(currentLine.ToString(), fontAsset);

                textComponent.Origin = textComponent.GetBounds().Size.ToVector2() / 2;

                lines.Add(textComponent);
            }

            AddRange(lines);
        }
    }
}