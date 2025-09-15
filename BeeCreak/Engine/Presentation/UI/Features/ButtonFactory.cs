using BeeCreak.Engine.Asset;
using BeeCreak.Engine.Presentation.UI.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Presentation
{
    public class ButtonFactory
    {
        private readonly AssetManager assetManager;

        public ButtonFactory(AssetManager assetManager)
        {
            this.assetManager = assetManager;
        }

        public ButtonComponent CreateButton(string text, string spriteSheetId, string fontId, Action action)
        {
            return new ButtonComponent(
                new ButtonProps(
                    assetManager.Acquire<SpriteSheet>(spriteSheetId),
                    assetManager.Acquire<SpriteFont>(fontId),
                    text,
                    action
                )
            );
        }
    }
}