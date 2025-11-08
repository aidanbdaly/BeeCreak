using BeeCreak.App.Game.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Domain.Tile
{
    public class TileMap(List<TileReference> tileMap)
    {
        public List<TileReference> tiles = tileMap;

        public IEnumerable<TileReference> WithNeighbours(Point point)
        {
            foreach (var offset in TileUtils.WithNeighborOffsets)
            {
                var neighborPoint = new Point(point.X + offset.X, point.Y + offset.Y);
                var neighborTile = tiles.FirstOrDefault(t => t.Position.Equals(neighborPoint));
                yield return neighborTile;
            }
        }
        public void Variate(Point point)
        {
            var tile = tiles.FirstOrDefault(t => t.Position.Equals(point));

            int mask = 0;
            int index = 0;

            foreach (var offset in TileUtils.NeighborOffsets)
            {
                var neighborPoint = new Point(point.X + offset.X, point.Y + offset.Y);
                var neighbor = tiles.FirstOrDefault(t => t.Position.Equals(neighborPoint));

                if (neighbor.Base.Id == tile.Base.Id)
                {
                    mask |= 1 << (7 - index);
                }

                index++;
            }

            tile.Variant = TileUtils.TileVariantLookup.TryGetValue(mask, out var tileVariant)
                ? tileVariant
                : TileVariant.Default;
        }
    }
}
