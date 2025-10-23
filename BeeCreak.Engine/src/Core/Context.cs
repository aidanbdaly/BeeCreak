using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Core
{
    public class Context
    {
        public AssetManager assetManager;

        public GraphicsDevice graphicsDevice;

        public InputManager inputManager;

        public string saveId;

        public Action<string> switchScene;

        public Func<Point> getMousePosition;

    }
}