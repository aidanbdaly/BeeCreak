using BeeCreak.Engine.Assets;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Components
{
    public class SpriteComponent : TextureComponent
    {
        private readonly AssetHandle<SpriteSheet> spriteSheetHandle;

        public SpriteComponent(AssetHandle<SpriteSheet> spriteSheetHandle, string defaultSprite) : base(spriteSheetHandle.Asset.Image)
        {
            this.spriteSheetHandle = spriteSheetHandle;
            SetSprite(defaultSprite);
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(
                (int)WorldTransform.Position.X - (int)Origin.X,
                (int)WorldTransform.Position.Y - (int)Origin.Y,
                (int)(SourceRectangle?.Width ?? 0 * WorldTransform.Scale),
                (int)(SourceRectangle?.Height ?? 0 * WorldTransform.Scale)
            );
        }

        public void SetSprite(string spriteName)
        {
            if (spriteSheetHandle.Asset.Frames.TryGetValue(spriteName, out var frame))
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