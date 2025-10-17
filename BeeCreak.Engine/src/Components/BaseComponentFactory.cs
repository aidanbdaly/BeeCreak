using System.Text;
using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public class BaseComponentFactory
    {
        private readonly SceneServices services;

        public BaseComponentFactory(SceneServices services)
        {
            this.services = services;
        }

        public ButtonComponent Button(string text, string spriteSheetId, string fontId, Action action)
        {
            return new ButtonComponent(
                    text,
                    services.AssetManager.Acquire<SpriteSheet>(spriteSheetId),
                    services.AssetManager.Acquire<SpriteFont>(fontId),
                    action,
                    services.GetMousePosition
            );
        }

        public CachedComponentCollection CachedComponentCollection(IEnumerable<IComponent> components)
        {
            return new CachedComponentCollection(services.GraphicsDevice, components);
        }

        public TextComponent Text(string text, string fontId)
        {
            return new TextComponent(text, services.AssetManager.Acquire<SpriteFont>(fontId));
        }

        public static ComponentCollection ComponentArray(int spacing, IEnumerable<IComponent> components)
        {
            var maxHeight = components.Sum(c => c.GetBounds().Height) + (spacing * (components.Count() - 1));

            components.ToList().Select<Component>(c => c.UpdateLocalTransform(new Vector2(0, -maxHeight / 2 + c.GetBounds().Height / 2)));

            return new ComponentCollection(components);
        }

        public ComponentCollection Paragraph(string text, int maxWidth, string fontId)
        {
            var currentLine = new StringBuilder();
            var lines = new List<TextComponent>();
            var font = services.AssetManager.Acquire<SpriteFont>(fontId);

            foreach (var word in text.Split(" "))
            {
                if (font.Asset.MeasureString(new StringBuilder().Append(currentLine).Append($"{word} ").ToString()).X > maxWidth)
                {
                    var textComponent = new TextComponent(currentLine.ToString(), font);

                    textComponent.Origin = textComponent.GetBounds().Size.ToVector2() / 2;

                    lines.Add(textComponent);

                    currentLine.Clear();
                }

                currentLine.Append($"{word} ");
            }

            if (currentLine.Length > 0)
            {
                var textComponent = new TextComponent(currentLine.ToString(), font);

                textComponent.Origin = textComponent.GetBounds().Size.ToVector2() / 2;

                lines.Add(textComponent);
            }

            return ComponentArray(0, lines);
        }
    }
}