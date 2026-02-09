using System.Collections.Immutable;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public class Scene(
    Dictionary<Type, Func<App, object>> services,
    List<Func<App, IGameComponent>> components,
    Point canvasSize,
    Action<App> onBeginRun
) : IScene
    {
        public ImmutableList<Func<App, IGameComponent>> Components { get; } = [.. components];

        public ImmutableDictionary<Type, Func<App, object>> Services { get; } = services.ToImmutableDictionary();

        public Point CanvasSize { get; } = canvasSize;

        public Action<App> OnBeginRun { get; } = onBeginRun;
    }
}