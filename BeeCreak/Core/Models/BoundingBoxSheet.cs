using System.Collections.Immutable;

namespace BeeCreak.Core.Models;

public record BoundingBoxSheet(
    string Id,
    ImmutableDictionary<string, ImmutableRectangle> BoundingBoxes
);
