using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Models;

public record TileMapRecord(
    string Id,
    SpriteSheet SpriteSheet,
    BoundingBoxSheet BoundingBoxSheet,
    Dictionary<Point, string> Tiles
);