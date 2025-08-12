using BeeCreak.Shared.Services;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak
{
    public class SpriteComponent : TextureComponent
    {
        private readonly AssetHandle<SpriteSheet> spriteSheetHandle;

        public SpriteComponent(AssetHandle<SpriteSheet> spriteSheetHandle) : base(spriteSheetHandle.Asset.Image)
        {
            this.spriteSheetHandle = spriteSheetHandle;
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
            spriteSheetHandle?.Dispose();
        }
    }
}