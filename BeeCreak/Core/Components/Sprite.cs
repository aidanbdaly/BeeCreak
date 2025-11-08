using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Core.Components
{
    public class Sprite : Texture
    {
        private readonly SpriteSheet spriteSheet;

        public Sprite(SpriteSheet spriteSheet, string defaultSprite) : base(spriteSheet.Image)
        {
            this.spriteSheet = spriteSheet;
            SetSprite(defaultSprite);
        }

        public override Rectangle GetBounds()
        {
            var width = (SourceRectangle?.Width ?? 0) * Scale;
            var height = (SourceRectangle?.Height ?? 0) * Scale;

            return new Rectangle(
                (int)(Position.X - Origin.X),
                (int)(Position.Y - Origin.Y),
                (int)width,
                (int)height
            );
        }

        public void SetSprite(string spriteName)
        {
            if (spriteSheet.Frames.TryGetValue(spriteName, out var frame))
            {
                SourceRectangle = frame;
            }
            else
            {
                throw new ArgumentException($"Sprite '{spriteName}' not found in sprite sheet.");
            }
        }

        public override void Dispose()
        {
            spriteSheet.Dispose();
        }
    }
}
