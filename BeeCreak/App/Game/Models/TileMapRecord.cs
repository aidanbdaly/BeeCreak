using System.Collections.Generic;
using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Models;

public record TileMapRecord(
    string Id,
    SpriteSheet SpriteSheet,
    Dictionary<Point, string> Tiles
);
