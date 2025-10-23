
namespace BeeCreak
{
    using global::BeeCreak.Shared.Data.Models;
    using Microsoft.Xna.Framework;

    public class TileState
    {
        public string ContentId { get; set; } = string.Empty;

        public TileVariant Variant { get; set; }

        public Point Position { get; set; }
    }
}
