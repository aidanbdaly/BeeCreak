using BeeCreak.Engine.Types;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Domain.Entity
{
    public class EntityState
    {
        public State<string> AnimationName { get; set; } = new(string.Empty);

        public State<Vector2> Position { get; set; } = new(Vector2.Zero);
    }
}
