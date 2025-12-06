using System.Collections.Immutable;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Data.Models
{
    public record TextSet(
        string Id,
        SpriteFont Font,
        ImmutableDictionary<string, string> Data
    );
}