using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions;

[ContentProcessor(DisplayName = "Entity Attributes Processor")]
public sealed class EntityAttributesProcessor : ContentProcessor<EntityAttributesDTO, EntityAttributesContent>
{
    public override EntityAttributesContent Process(EntityAttributesDTO input, ContentProcessorContext context)
    {
        var entityAttributesContent = new EntityAttributesContent
        {
            BaseVelocity = input.BaseVelocity,
            Controlled = input.Controlled,
            HitBox = input.HitBox
        };

        return entityAttributesContent;
    }
}
