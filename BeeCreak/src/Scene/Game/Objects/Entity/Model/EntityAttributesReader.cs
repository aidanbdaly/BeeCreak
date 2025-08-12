using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BeeCreak.src.Models
{
    
    public class EntityAttributesReader : ContentTypeReader<EntityAttributes>
    {
        protected override EntityAttributes Read(ContentReader input, EntityAttributes existingInstance)
        {
            var baseVelocity = input.ReadSingle();
            var controlled = input.ReadBoolean();
            var hitBoxX = input.ReadInt32();
            var hitBoxY = input.ReadInt32();
            var hitBoxWidth = input.ReadInt32();
            var hitBoxHeight = input.ReadInt32();
    
            return new EntityAttributes
            {
                BaseVelocity = baseVelocity,
                Controlled = controlled,
                HitBox = new Rectangle(hitBoxX, hitBoxY, hitBoxWidth, hitBoxHeight)
            };
        }
    }
}
