using System.Collections.Immutable;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Services
{
    public interface IScene
    {
        ImmutableList<Func<App, IGameComponent>> Components { get; }

        Point Resolution { get; }
    }
}