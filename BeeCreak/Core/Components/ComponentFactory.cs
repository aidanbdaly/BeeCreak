using BeeCreak.Core.Models;
using BeeCreak.Core;
using BeeCreak.Core.Transitions;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Components
{
    public class ComponentFactory(Context context)
    {
        private readonly Context context = context;

        public Button Button(string text, string spriteSheetId, string fontId, Action action)
        {
            return new Button(
                    text,
                    context.assetManager.Acquire<SpriteSheet>("Spritesheet/" + spriteSheetId),
                    context.assetManager.Acquire<SpriteFont>("Font/" + fontId),
                    action,
                    context.input.GetMousePosition,
                    context.input.PointerButtonCycled
            );
        }

        public Text Text(string text, string fontId, int? maxWidth = null)
        {
            return new Text(text, context.assetManager.Acquire<SpriteFont>("Font/" + fontId), maxWidth);
        }

        public Sprite Sprite(string spriteSheetId, string frame = null)
        {
            return new Sprite(context.assetManager.Acquire<SpriteSheet>(spriteSheetId), frame);
        }

        public FadeInTransition FadeInTransition(float duration)
        {
            return new FadeInTransition(context.graphicsDevice, duration);
        }

        public FadeOutTransition FadeOutTransition(float duration)
        {
            return new FadeOutTransition(context.graphicsDevice, duration);
        }
    }
}
