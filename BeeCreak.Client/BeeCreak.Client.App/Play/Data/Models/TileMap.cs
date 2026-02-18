using BeeCreak.Engine.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Models;

public record TileMap(
    string Id,
    SpriteSheet SpriteSheet,
    BoundingBoxSheet BoundingBoxSheet,
    Dictionary<Point, string> Data);