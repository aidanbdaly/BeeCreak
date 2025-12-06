using System.Collections.Immutable;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public class Scene(
    List<Func<App, IGameComponent>> components,
    Point resolution
) : IScene
    {
        public ImmutableList<Func<App, IGameComponent>> Components { get; } = [.. components];

        public Point Resolution { get; } = resolution;
    }
}