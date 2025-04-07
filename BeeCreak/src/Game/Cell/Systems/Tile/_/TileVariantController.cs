using BeeCreak.Shared.Data.Models;

namespace BeeCreak.Scene.Main;

public class TileAssetVariantCalculator
{
    public TileAssetVariantCalculator()
    {
    }

    private ITile[,] Tiles { get; set; }

    public void SetTiles(ITile[,] tiles)
    {
        Tiles = tiles;
    }

    public void Recalculate()
    {
        for (var x = 0; x < Tiles.GetLength(0); x++)
        {
            for (var y = 0; y < Tiles.GetLength(1); y++)
            {
                Recalculate(x, y);
            }
        }
    }

    public void Recalculate(int x, int y)
    {
        var tile = Tiles[x, y];

        if (!TileAssetTypeExtensions.HasVariants(tile.Type))
        {
            return;
        }

        var adjacentTilePositions = new (int, int)[8]
            {
                (-1, -1),
                (0, -1),
                (1, -1),
                (-1, 0),
                (1, 0),
                (-1, 1),
                (0, 1),
                (1, 1),
            };

        var adjacentTiles = new ITile[8];

        var adjacentTileConfiguration = new int[8];

        for (var i = 0; i < 8; i++)
        {
            var (adjacentTileX, adjacentTileY) = adjacentTilePositions[i];

            if (IsInWorld(x + adjacentTileX, y + adjacentTileY, Tiles.GetLength(0)))
            {
                adjacentTiles[i] = Tiles[x + adjacentTileX, y + adjacentTileY];
            }
        }

        for (var i = 0; i < 8; i++)
        {
            var adjacentTile = adjacentTiles[i];

            if (adjacentTile == null)
            {
                continue;
            }

            var matchesTileType = tile.Type == adjacentTile.Type ? 1 : 0;

            adjacentTileConfiguration[i] = matchesTileType;
        }

        int mask = 0;

        for (var i = 0; i < 8; i++)
        {
            mask |= adjacentTileConfiguration[i] << 7 - i;
        }

        var variant = GetTileAssetVariant(mask);

        tile.Variant = variant;
    }

    private static TileAssetVariant GetTileAssetVariant(int mask)
    {
        var variant = mask switch
        {
            0b00101111 => TileAssetVariant.TopLeftOuter,
            0b01111111 => TileAssetVariant.TopLeftInner,
            0b00101011 => TileAssetVariant.TopLeftOuter,
            0b00001111 => TileAssetVariant.TopLeftOuter,
            0b10010110 => TileAssetVariant.TopRightOuter,
            0b10010111 => TileAssetVariant.TopRightOuter,
            0b00010110 => TileAssetVariant.TopRightOuter,
            0b00010111 => TileAssetVariant.TopRightOuter,
            0b11011111 => TileAssetVariant.TopRightInner,
            0b01101011 => TileAssetVariant.Left,
            0b11101111 => TileAssetVariant.Left,
            0b01101001 => TileAssetVariant.BottomLeftOuter,
            0b11111011 => TileAssetVariant.BottomLeftInner,
            0b11101001 => TileAssetVariant.BottomLeftOuter,
            0b11101000 => TileAssetVariant.BottomLeftOuter,
            0b11111000 => TileAssetVariant.Bottom,
            0b11111001 => TileAssetVariant.Bottom,
            0b11111100 => TileAssetVariant.Bottom,
            0b11111101 => TileAssetVariant.Bottom,
            0b11010110 => TileAssetVariant.Right,
            0b11110110 => TileAssetVariant.Right,
            0b11010111 => TileAssetVariant.Right,
            0b11110111 => TileAssetVariant.Right,
            0b01101111 => TileAssetVariant.Left,
            0b11101011 => TileAssetVariant.Left,
            0b11110001 => TileAssetVariant.BottomRightOuter,
            0b11110000 => TileAssetVariant.BottomRightOuter,
            0b11010100 => TileAssetVariant.BottomRightOuter,
            0b11111110 => TileAssetVariant.BottomRightInner,
            0b11110101 => TileAssetVariant.BottomRightOuter,
            0b11110100 => TileAssetVariant.BottomRightOuter,
            0b11010000 => TileAssetVariant.BottomRightOuter,
            0b10111111 => TileAssetVariant.Top,
            0b00011111 => TileAssetVariant.Top,
            0b00111111 => TileAssetVariant.Top,
            0b10011111 => TileAssetVariant.Top,
            _ => TileAssetVariant.Default,
        };

        return variant;
    }
    private static bool IsInWorld(int x, int y, int size)
    {
        return x >= 0 && x < size && y >= 0 && y < size;
    }
}
