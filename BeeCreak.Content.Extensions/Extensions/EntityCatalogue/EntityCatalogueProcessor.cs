

using System.Linq;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentProcessor(DisplayName = "Entity Catalogue Processor")]

public sealed class EntityCatalogueProcessor : ContentProcessor<EntityCatalogueDTO, EntityCatalogueContent>
{
    public override EntityCatalogueContent Process(EntityCatalogueDTO input, ContentProcessorContext context)
    {
        var entityCatalogueContent = new EntityCatalogueContent();

        foreach (var entityType in input)
        {
            var entityTypeContent = new EntityTypeContent
            {
                Default = new EntityAttributesContent
                {
                    HitBox = entityType.Value.Default.HitBox,
                    BaseVelocity = entityType.Value.Default.BaseVelocity,
                    Controlled = entityType.Value.Default.Controlled,
                },

                Variants = entityType.Value.Variants.ToDictionary(
                    variant => variant.Key,
                    variant => new EntityAttributesContent
                    {
                        HitBox = variant.Value.HitBox,
                        BaseVelocity = variant.Value.BaseVelocity,
                        Controlled = variant.Value.Controlled,
                    })
            };

            entityCatalogueContent[entityType.Key] = entityTypeContent;
        }

        return entityCatalogueContent;
    }
}