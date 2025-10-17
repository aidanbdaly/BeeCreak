using BeeCreak.Engine.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public class TextComponent : Component
    {
        private readonly AssetHandle<SpriteFont> fontAsset;

        private readonly string text;

        public TextComponent(string text, AssetHandle<SpriteFont> fontAsset)
        {
            this.fontAsset = fontAsset;
            this.text = text;
        }

        public override Rectangle GetBounds()
        {
            var size = fontAsset.Asset.MeasureString(text) * WorldTransform.Scale;

            return new Rectangle(
                (int)WorldTransform.Position.X,
                (int)WorldTransform.Position.Y,
                (int)size.X,
                (int)size.Y
            );
        }

        public override void Dispose() => fontAsset.Dispose();
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(
                fontAsset.Asset,
                text,
                WorldTransform.Position,
                Color.White,
                WorldTransform.Rotation,
                Origin,
                WorldTransform.Scale,
                SpriteEffects.None,
                0f);
        }
    }
}