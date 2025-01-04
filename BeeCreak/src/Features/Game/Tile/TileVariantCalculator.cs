using BeeCreak.Game.Scene.Tile;

namespace BeeCreak.Features.Game.Tile;

public class TileVariantCalculator : ITileVariantCalculator
{
    public TileVariantCalculator()
    {
    }

    private ITile[,] Tiles { get; set; }

    public void SetTiles(ITile[,] tiles)
    {
        Tiles = tiles;
    }

    public void RecalculateTileVariants()
    {
        for (var x = 0; x < Tiles.GetLength(0); x++)
        {
            for (var y = 0; y < Tiles.GetLength(1); y++)
            {
                RecalculateTileVariant(x, y);
            }
        }
    }

    public void RecalculateTileVariant(int x, int y)
    {
        var tile = Tiles[x, y];

        if (tile.Type.HasVariant == false)
        {
            return;
        }

        var adjacentTilePositions = new (int, int)[8]
            {
                (-1, 1),
                (0, 1),
                (1, 1),
                (-1, 0),
                (1, 0),
                (-1, -1),
                (0, -1),
                (1, -1),
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

            var matchesTileType = tile.Type.Id == adjacentTile.Type.Id ? 1 : 0;

            adjacentTileConfiguration[i] = matchesTileType;
        }

        int mask = 0;

        for (var i = 0; i < 8; i++)
        {
            mask |= adjacentTileConfiguration[i] << 7 - i;
        }

        var variant = GetTileVariant(mask);

        tile.SetVariant(variant);
    }

    private static TileVariant GetTileVariant(int mask)
    {
        var variant = mask switch
        {
            0b11111110 => TileVariant.TopLeftInner,
            0b11111101 => TileVariant.Bottom,
            0b11111011 => TileVariant.TopRightInner,
            0b11110111 => TileVariant.Left,
            0b11101111 => TileVariant.Right,
            0b11011111 => TileVariant.BottomLeftInner,
            0b10111111 => TileVariant.Top,
            0b01111111 => TileVariant.BottomRightInner,
            0b11111100 => TileVariant.Bottom,
            0b11111001 => TileVariant.Bottom,
            0b10011111 => TileVariant.Top,
            0b00111111 => TileVariant.Top,
            0b11111000 => TileVariant.Bottom,
            0b11110001 => TileVariant.TopLeftOuter,
            0b10001111 => TileVariant.BottomRightOuter,
            0b00011111 => TileVariant.Top,
            0b11110000 => TileVariant.TopLeftOuter,
            0b00001111 => TileVariant.BottomRightOuter,
            _ => TileVariant.Center,
        };

        return variant;
    }
    private static bool IsInWorld(int x, int y, int size)
    {
        return x >= 0 && x < size && y >= 0 && y < size;
    }
}
