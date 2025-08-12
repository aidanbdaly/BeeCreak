using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak
{
    
    public class TileUtils
    {
        public static readonly Point[] WithNeighborOffsets =
        [
            new(0, 0), new(1, 0), new(0, 1),
            new(1, 1), new(-1, 0), new(0, -1),
            new(-1, -1), new(-1, 1), new(1, -1)
        ];
        public static readonly Point[] NeighborOffsets =
        [
            new(-1, -1), new(0, -1), new(1, -1),
            new(-1, 0),               new(1, 0),
            new(-1, 1),  new(0, 1),  new(1, 1)
        ];
    
        public static readonly IEnumerable<(int, Point)> NeighborOffsetsWithIndex =
            NeighborOffsets.Select((offset, index) => (index, offset));
    
        public static readonly Dictionary<int, TileVariant> TileVariantLookup = new()
        {
            { 0b00101111, TileVariant.TopLeftOuter },
            { 0b00101011, TileVariant.TopLeftOuter },
            { 0b00001111, TileVariant.TopLeftOuter },
            { 0b01111111, TileVariant.TopLeftInner },
            { 0b10010110, TileVariant.TopRightOuter },
            { 0b10010111, TileVariant.TopRightOuter },
            { 0b00010110, TileVariant.TopRightOuter },
            { 0b00010111, TileVariant.TopRightOuter },
            { 0b11011111, TileVariant.TopRightInner },
            { 0b01101011, TileVariant.Left },
            { 0b11101111, TileVariant.Left },
            { 0b01101111, TileVariant.Left },
            { 0b11101011, TileVariant.Left },
            { 0b11111011, TileVariant.BottomLeftInner },
            { 0b01101001, TileVariant.BottomLeftOuter },
            { 0b11101001, TileVariant.BottomLeftOuter },
            { 0b11101000, TileVariant.BottomLeftOuter },
            { 0b11111000, TileVariant.Bottom },
            { 0b11111001, TileVariant.Bottom },
            { 0b11111100, TileVariant.Bottom },
            { 0b11111101, TileVariant.Bottom },
            { 0b11010110, TileVariant.Right },
            { 0b11110110, TileVariant.Right },
            { 0b11010111, TileVariant.Right },
            { 0b11110111, TileVariant.Right },
            { 0b11110000, TileVariant.BottomRightOuter },
            { 0b11110001, TileVariant.BottomRightOuter },
            { 0b11110100, TileVariant.BottomRightOuter },
            { 0b11110101, TileVariant.BottomRightOuter },
            { 0b11010100, TileVariant.BottomRightOuter },
            { 0b11010000, TileVariant.BottomRightOuter },
            { 0b11111110, TileVariant.BottomRightInner },
            { 0b10111111, TileVariant.Top },
            { 0b00011111, TileVariant.Top },
            { 0b00111111, TileVariant.Top },
            { 0b10011111, TileVariant.Top },
        };
    }
}
