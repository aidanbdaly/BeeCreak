
using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Core
{
    public interface IScene : IUpdateable, IDrawable, IDisposable
    {
        Color Clear { get; set; }

        Transition EntranceTransition { get; init; }

        Transition ExitTransition { get; init; }

        int Width { get; init; }

        int Height { get; init; }
    }
}