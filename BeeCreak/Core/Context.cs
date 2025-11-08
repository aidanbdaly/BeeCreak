using BeeCreak.Core.Input;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core
{
    public class Context(
        ContentManager contentManager,
        GraphicsDevice graphicsDevice,
        Input input,
        SceneManager sceneManager
        )
    {
        public readonly ContentManager content = contentManager;

        public readonly GraphicsDevice graphicsDevice = graphicsDevice;

        public readonly Input input = input;

        public readonly SceneManager sceneManager = sceneManager;

        public string SaveId { get; set; }
    }
}