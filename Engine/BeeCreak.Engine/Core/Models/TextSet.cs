using System.Collections.Immutable;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Core.Models
{
    public record TextSet(
        string Id,
        SpriteFont Font,
        ImmutableDictionary<string, string> Data
    );
}