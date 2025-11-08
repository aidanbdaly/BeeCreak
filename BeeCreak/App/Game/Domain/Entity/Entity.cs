using BeeCreak.App.Game.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Domain.Entity
{
    public class Entity(EntityReference reference) : EntityBase(reference)
    {
        public override void Update(GameTime gameTime)
        {
            foreach (var behaviour in Reference.Base.Behaviours)
            {
                behaviour.Update(gameTime);
            }
        }
    }
}
