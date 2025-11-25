using BeeCreak.Extensions.Entity.Model;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.Entity.Reference;

[ContentProcessor(DisplayName = EntityReferenceConfig.ProcessorDisplayName)]
public sealed class EntityReferenceProcessor : ContentProcessor<EntityReferenceDTO, EntityReferenceContent>
{
    public override EntityReferenceContent Process(EntityReferenceDTO input, ContentProcessorContext context)
    {
        AssertValid(input);

        var model = EntityModelLoader.Load(input.Model, context);

        var content = new EntityReferenceContent
        {
            Id = input.Id,
            Model = model,
        };

        return content;
    }

    private static void AssertValid(EntityReferenceDTO input)
    {
        if (input is null)
        {
            throw new InvalidContentException("Entity reference payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Entity reference requires an id.");
        }

        if (string.IsNullOrWhiteSpace(input.Model))
        {
            throw new InvalidContentException($"Entity reference requires a base entity id.");
        }
    }
}
