using BeeCreak.Engine.Assets;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Components
{
    public class Sprite : Texture
    {
        private readonly Asset<SpriteSheet> spriteSheetHandle;

        public Sprite(Asset<SpriteSheet> spriteSheetHandle, string defaultSprite) : base(spriteSheetHandle.Value.Image)
        {
            this.spriteSheetHandle = spriteSheetHandle;
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
            if (spriteSheetHandle.Value.Frames.TryGetValue(spriteName, out var frame))
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
            spriteSheetHandle.Dispose();
        }
    }
}
