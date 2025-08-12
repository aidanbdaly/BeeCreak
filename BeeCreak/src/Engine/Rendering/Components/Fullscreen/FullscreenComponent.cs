using BeeCreak.Shared.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class FullscreenComponent : TextureComponent, IResponsive
    {
        private readonly AssetHandle<Texture2D> textureAsset;

        public FullscreenComponent(AssetHandle<Texture2D> textureAsset) : base(textureAsset.Asset) {
            this.textureAsset = textureAsset;
        }

        public void Layout(GameWindow window)
        {
            var clientBounds = window.ClientBounds;
            var textureBounds = GetTextureBounds();

            float wR = clientBounds.Width / (float)textureBounds.Width;
            float hR = clientBounds.Height / (float)textureBounds.Height;

            Scale = Math.Max(wR, hR);

            Position = new Vector2(
                (clientBounds.Width - textureBounds.Width) * 0.5f,
                (clientBounds.Height - textureBounds.Height) * 0.5f
            );
        }

        public override void Dispose()
        {
            textureAsset.Dispose();
        }
    }
}