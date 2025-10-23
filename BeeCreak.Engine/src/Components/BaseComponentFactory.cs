using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Core;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public class BaseComponentFactory
    {
        private readonly Context context;

        public BaseComponentFactory(Context context)
        {
            this.context = context;
        }

        public Button Button(string text, string spriteSheetId, string fontId, Action action)
        {
            return new Button(
                    text,
                    context.assetManager.Acquire<SpriteSheet>(spriteSheetId),
                    context.assetManager.Acquire<SpriteFont>(fontId),
                    action,
                    context.getMousePosition
            );
        }

        public Text Text(string text, string fontId, int? maxWidth = null)
        {
            return new Text(text, context.assetManager.Acquire<SpriteFont>(fontId), maxWidth);
        }

        public Sprite Sprite(string spriteSheetId, string frame = null)
        {
            return new Sprite(context.assetManager.Acquire<SpriteSheet>(spriteSheetId), frame);
        }
    }
}
