using BeeCreak.Engine.Asset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Presentation.Primitives
{
    public class TextComponent : Component
    {
        private readonly AssetHandle<SpriteFont> fontAsset;

        public TextComponent(string text, AssetHandle<SpriteFont> fontAsset)
        {
            this.fontAsset = fontAsset;
            Text = text;
        }

        public string Text { get; set; }

        public override Rectangle GetBounds()
        {
            var size = fontAsset.Asset.MeasureString(Text) * WorldTransform.Scale;

            return new Rectangle(
                (int)WorldTransform.Position.X,
                (int)WorldTransform.Position.Y,
                (int)size.X,
                (int)size.Y
            );
        }

        public override void Dispose()
        {
            fontAsset.Dispose();
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(fontAsset.Asset, Text, WorldTransform.Position, Color.White, WorldTransform.Rotation, Origin, WorldTransform.Scale, SpriteEffects.None, 0f);
        }
    }
}