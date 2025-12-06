using System.Collections.Immutable;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Data.Models
{
    public record BoundingBoxSheet(string Id, ImmutableDictionary<string, Rectangle> BoundingBoxes);
}
