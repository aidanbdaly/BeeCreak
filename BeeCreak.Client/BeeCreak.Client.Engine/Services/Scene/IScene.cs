using System.Collections.Immutable;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public interface IScene
    {
        ImmutableDictionary<Type, Func<App, object>> Services { get; }

        Point CanvasSize { get; }

        Action<App> OnBeginRun { get; }
    }
}