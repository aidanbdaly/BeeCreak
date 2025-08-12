using BeeCreak.Shared.Services;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak
{
    public class BaseComponentFactory
    {
        private readonly AssetManager assetManager;

        public BaseComponentFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }

        public ButtonComponent CreateButton(string text, Action action, string spriteSheetId)
        {
            return new ButtonComponent(
                text,
                action,
                assetManager.Acquire<SpriteSheet>(spriteSheetId)
            );
        }

        public FullscreenComponent CreateBackground(string textureId)
        {
            return new FullscreenComponent(assetManager.Acquire<Texture2D>(textureId));
        }
    }
}