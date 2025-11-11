using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Models
{
    public class EntityReference
    {
        public string Id { get; init; }

        public EntityRecord Base { get; init; }

        public CellReference Cell { get; set; }

        public string Variant { get; set; }

        public Vector2 Position { get; set; } = Vector2.Zero;
    }
}
