using BeeCreak.Core.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core
{
    public class Context(
        ContentManager contentManager,
        GraphicsDevice graphicsDevice,
        InputManager inputManager,
        SceneManager sceneManager
        )
    {
        public readonly ContentManager content = contentManager;

        public readonly GraphicsDevice graphicsDevice = graphicsDevice;

        public readonly InputManager inputManager = inputManager;

        public readonly SceneManager sceneManager = sceneManager;

        public string SaveId { get; set; } = string.Empty;
    }
}