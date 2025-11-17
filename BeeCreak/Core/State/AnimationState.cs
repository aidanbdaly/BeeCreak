using System.Collections.Immutable;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Core.State
{
    internal class AnimationState(ImmutableDictionary<string, ImmutableList<ImmutableRectangle>> animations)
    {
        internal State<Rectangle> SourceRectangle { get; init; } = new State<Rectangle>(default);

        internal State<string> AnimationName { get; init; } = new State<string>(string.Empty);

       
    }
}