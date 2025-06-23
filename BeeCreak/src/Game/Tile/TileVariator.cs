using BeeCreak.Shared.Data.Models;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class TileVariator
{
    private readonly TileManager tileManager;

    public TileVariator(TileManager tileManager)
    {
        this.tileManager = tileManager;

        tileManager.OnStateImported += (sender, e) =>
        {
            foreach (var (x, y, tile) in tileManager.Enumerate())
            {
                if (tile.Attributes.IsVariable) Variate(x, y, tile);
            }
        };
    }

    public void Variate(int x, int y, Tile tile)
    {
        int mask = 0;

        for (int i = 0; i < TileUtils.NeighborOffsets.Length; i++)
        {
            var neighbor = tileManager[new Point(x, y) + TileUtils.NeighborOffsets[i]];

            if (neighbor.State.Type == tile.State.Type)
            {
                mask |= 1 << (7 - i);
            }
        }

        var variant = TileVariantLookup.TryGetValue(mask, out var tileVariant)
            ? tileVariant
            : TileVariant.CenterForeground;

        if (tile.Variant != variant)
        {
            tile.Variant = variant;
        }
    }
}
