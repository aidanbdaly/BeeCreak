using BeeCreak.Engine.Asset;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak
{

    public class Entity
    {
        public event EventHandler OnStateChanged;

        public Entity(EntityState state, AssetHandle<EntityAttributes> attributes)
        {
            State = state;
            Attributes = attributes;
        }

        public EntityState State { get; }

        public AssetHandle<EntityAttributes> Attributes { get; }

        public Rectangle AbsoluteHitBox =>
            new(
                (int)State.Position.X,
                (int)State.Position.Y,
                (int)Attributes.Asset.HitBox.Width,
                (int)Attributes.Asset.HitBox.Height
            );
    }

}
