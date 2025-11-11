using Microsoft.Xna.Framework;

namespace BeeCreak.Core.Models;

public record BoundingBoxSheet(
    string Id,
    Dictionary<string, Rectangle> BoundingBoxes
);