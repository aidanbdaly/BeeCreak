using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Components;
using BeeCreak.Engine.Core;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Components
{
    public class ButtonFactory
    {
        private readonly AssetManager assetManager;
        private readonly SceneContext sceneContext;

        public ButtonFactory(AssetManager assetManager, SceneContext sceneContext)
        {
            this.assetManager = assetManager;
            this.sceneContext = sceneContext;
        }

        public ButtonComponent CreateButton(string text, string spriteSheetId, string fontId, Action action)
        {
            return new ButtonComponent(
                new ButtonProps(
                    assetManager.Acquire<SpriteSheet>(spriteSheetId),
                    assetManager.Acquire<SpriteFont>(fontId),
                    text,
                    action,
                    sceneContext.GetMousePosition
                )
            );
        }
    }
}