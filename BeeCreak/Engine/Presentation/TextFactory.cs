using BeeCreak.Engine.Asset;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Presentation
{
    public class TextFactory
    {
        private readonly AssetManager assetManager;

        public TextFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }

        public ParagraphComponent CreateParagraph(string text, int maxWidth)
        {
            return new ParagraphComponent(text, maxWidth, assetManager.Acquire<SpriteFont>("font/lookout"));
        }
    }
}