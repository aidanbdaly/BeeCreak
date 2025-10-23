using BeeCreak.Shared.Data.Models;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak;

public class TileVariator
{
    private readonly TileManager tileManager;

    public TileVariator(TileManager tileManager)
    {
        this.tileManager = tileManager;

        tileManager.OnStateImported += (sender, e) =>
        {
            foreach (var (x, y, tile) in tileManager.TileMap.Enumerate())
            {
                if (tile.Attributes.IsVariable) Variate(x, y, tile);
            }
        };
    }

    public void Variate(int x, int y, Tile tile)
    {
        int mask = 0;

        foreach (var (index, neighbor) in tileManager.Neighbors(new Point(x, y)))
        {
            if (neighbor.State.ContentId == tile.State.ContentId)
            {
                mask |= 1 << (7 - index);
            }
        }

        tile.State.Variant = TileUtils.TileVariantLookup.TryGetValue(mask, out var tileVariant)
            ? tileVariant
            : TileVariant.Default;
    }
}
