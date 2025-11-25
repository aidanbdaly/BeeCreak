using System.Collections.Immutable;
using Microsoft.Xna.Framework;

namespace BeeCreak.Core.Models;

public record BoundingBoxSheet(string Id, ImmutableDictionary<string, Rectangle> BoundingBoxes);
