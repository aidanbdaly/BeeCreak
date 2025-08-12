using BeeCreak.src.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak
{

    public class Entity
    {
        public event EventHandler OnStateChanged;

        public Entity(EntityState state, EntityAttributes attributes)
        {
            State = state;
            Attributes = attributes;
        }

        public EntityState State { get; }

        public EntityAttributes Attributes { get; }

        public Rectangle AbsoluteHitBox =>
            new(
                (int)State.Position.X,
                (int)State.Position.Y,
                (int)Attributes.HitBox.Width,
                (int)Attributes.HitBox.Height
            );
    }

}
