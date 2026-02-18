using BeeCreak.Engine.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine
{
    public interface IApp
    {
        IScreenService ScreenService { get; }

        SpriteBatch SpriteBatch { get; }

        IMouseInputService Mouse { get; }

        GraphicsDevice GraphicsDevice { get; }

        ContentManager Content { get; }

        GameServiceContainer Services { get; }
    }
}