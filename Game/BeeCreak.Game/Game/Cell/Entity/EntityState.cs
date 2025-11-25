using BeeCreak.Core.State;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Domain.Entity
{
    public class EntityState(State<string> animationName, State<Vector2> position)
    {
        public State<string> AnimationName { get; set; } = animationName;

        public State<Vector2> Position { get; set; } = position;
    }
}
