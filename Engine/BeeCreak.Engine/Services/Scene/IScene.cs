using System.Collections.Immutable;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public interface IScene
    {
        ImmutableDictionary<Type, Func<App, object>> Services { get; }

        ImmutableList<Func<App, IGameComponent>> Components { get; }

        Point Resolution { get; }

        Action<App> OnBeginRun { get; }
    }
}