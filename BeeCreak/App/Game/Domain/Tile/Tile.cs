using BeeCreak.App.Game.Models;
using BeeCreak.Core.Components;

namespace BeeCreak.App.Game.Domain.Tile
{
    public class Tile(TileReference reference) : Sprite(reference.Base.SpriteSheet, "")
    {
        private readonly TileReference reference = reference;
    }
}
