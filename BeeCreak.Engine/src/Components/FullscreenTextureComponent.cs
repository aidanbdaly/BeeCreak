using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Core;
using BeeCreak.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public class FullscreenTextureComponent : TextureComponent
    {
        private readonly AssetHandle<Texture2D> textureAsset;

        public FullscreenTextureComponent(AssetHandle<Texture2D> textureAsset) : base(textureAsset.Asset)
        {
            this.textureAsset = textureAsset;
        }

        public void Layout(GameWindow window)
        {
            var clientBounds = window.ClientBounds;
            var bounds = GetBounds();

            LocalTransform.Scale = Math.Max(clientBounds.Width / (float)bounds.Width, clientBounds.Height / (float)bounds.Height);
            LocalTransform.Position = new Vector2(
                clientBounds.Width / 2,
                clientBounds.Height / 2
            );

            Console.WriteLine($"FullscreenTextureComponent Layout: {LocalTransform.Position}, {LocalTransform.Scale}");
        }

        public override void Dispose()
        {
            textureAsset.Dispose();
        }
    }
}