using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class TileVariantSystem
{
    private readonly TileManager tileManager;

    private static readonly Point[] AdjacentOffsets =
    {
        new(-1, -1), new(0, -1), new(1, -1),
        new(-1, 0),               new(1, 0),
        new(-1, 1),  new(0, 1),  new(1, 1)
    };

    private static readonly Dictionary<int, TileAssetVariant> TileVariantLookup = new()
    {
        { 0b00101111, TileAssetVariant.TopLeftOuter },
        { 0b00101011, TileAssetVariant.TopLeftOuter },
        { 0b00001111, TileAssetVariant.TopLeftOuter },
        { 0b01111111, TileAssetVariant.TopLeftInner },
        { 0b10010110, TileAssetVariant.TopRightOuter },
        { 0b10010111, TileAssetVariant.TopRightOuter },
        { 0b00010110, TileAssetVariant.TopRightOuter },
        { 0b00010111, TileAssetVariant.TopRightOuter },
        { 0b11011111, TileAssetVariant.TopRightInner },
        { 0b01101011, TileAssetVariant.Left },
        { 0b11101111, TileAssetVariant.Left },
        { 0b01101111, TileAssetVariant.Left },
        { 0b11101011, TileAssetVariant.Left },
        { 0b11111011, TileAssetVariant.BottomLeftInner },
        { 0b01101001, TileAssetVariant.BottomLeftOuter },
        { 0b11101001, TileAssetVariant.BottomLeftOuter },
        { 0b11101000, TileAssetVariant.BottomLeftOuter },
        { 0b11111000, TileAssetVariant.Bottom },
        { 0b11111001, TileAssetVariant.Bottom },
        { 0b11111100, TileAssetVariant.Bottom },
        { 0b11111101, TileAssetVariant.Bottom },
        { 0b11010110, TileAssetVariant.Right },
        { 0b11110110, TileAssetVariant.Right },
        { 0b11010111, TileAssetVariant.Right },
        { 0b11110111, TileAssetVariant.Right },
        { 0b11110000, TileAssetVariant.BottomRightOuter },
        { 0b11110001, TileAssetVariant.BottomRightOuter },
        { 0b11110100, TileAssetVariant.BottomRightOuter },
        { 0b11110101, TileAssetVariant.BottomRightOuter },
        { 0b11010100, TileAssetVariant.BottomRightOuter },
        { 0b11010000, TileAssetVariant.BottomRightOuter },
        { 0b11111110, TileAssetVariant.BottomRightInner },
        { 0b10111111, TileAssetVariant.Top },
        { 0b00011111, TileAssetVariant.Top },
        { 0b00111111, TileAssetVariant.Top },
        { 0b10011111, TileAssetVariant.Top },
    };

    public TileVariantSystem(TileManager tileManager)
    {
        this.tileManager = tileManager;
        tileManager.TileTypeChanged += (sender, e) => CalculateVariant(e.Tile);
    }

    public void CalculateVariant(Tile tile)
    {
        if (tile == null) return;

        var tileSize = tileManager.Tiles.GetLength(0);
        int mask = 0;

        for (int i = 0; i < AdjacentOffsets.Length; i++)
        {
            var offset = AdjacentOffsets[i];
            var adjacentCoord = tile.Coordinate + offset;

            if (IsInsideWorld(adjacentCoord, tileSize))
            {
                var adjacentTile = tileManager.Tiles[adjacentCoord.X, adjacentCoord.Y];
                if (adjacentTile != null && adjacentTile.Type == tile.Type)
                {
                    mask |= 1 << (7 - i);
                }
            }
        }

        var variant = TileVariantLookup.TryGetValue(mask, out var tileVariant)
            ? tileVariant
            : TileAssetVariant.Default;

        tileManager.UpdateVariant(tile.Coordinate.X, tile.Coordinate.Y, variant.ToString());
    }

    private static bool IsInsideWorld(Point coordinate, int size)
    {
        return coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < size && coordinate.Y < size;
    }
}
