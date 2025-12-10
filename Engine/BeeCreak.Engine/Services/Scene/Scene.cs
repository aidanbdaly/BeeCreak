using System.Collections.Immutable;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public class Scene(
    Dictionary<Type, Func<App, object>> services,
    List<Func<App, IGameComponent>> components,
    Point resolution,
    Action<App> onBeginRun
) : IScene
    {
        public ImmutableList<Func<App, IGameComponent>> Components { get; } = [.. components];

        public ImmutableDictionary<Type, Func<App, object>> Services { get; } = services.ToImmutableDictionary();

        public Point Resolution { get; } = resolution;

        public Action<App> OnBeginRun { get; } = onBeginRun;
    }
}