using BeeCreak.App.Game.Models;
using BeeCreak.Core.Components;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Domain.Entity
{
    public abstract class EntityBase(
        EntityReference reference
    ) : Sprite(reference.Base.SpriteSheet, ""), IEntity, IUpdateable
    {
        public EntityReference Reference => reference;

        public abstract void Update(GameTime gameTime);
    }
}
