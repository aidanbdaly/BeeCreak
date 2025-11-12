using System.Collections.Immutable;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Models;

public record TileMapRecord(
    string Id,
    SpriteSheet SpriteSheet,
    BoundingBoxSheet BoundingBoxSheet,
    ImmutableDictionary<Point, string> Tiles
);
