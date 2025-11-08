using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.EntityReference;

[ContentProcessor(DisplayName = "Entity Reference Processor")]
public sealed class EntityReferenceProcessor : ContentProcessor<EntityReferenceDto, EntityReferenceContent>
{
    public override EntityReferenceContent Process(EntityReferenceDto input, ContentProcessorContext context)
    {
        if (input is null)
        {
            throw new InvalidContentException("Entity reference payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Entity reference requires an id.");
        }

        if (input.Position is null)
        {
            throw new InvalidContentException($"Entity reference '{input.Id}' requires a position.");
        }

        var content = new EntityReferenceContent
        {
            Id = input.Id,
            BaseId = input.Base,
            CellId = input.Cell,
            Variant = string.IsNullOrWhiteSpace(input.Variant) ? "default" : input.Variant,
            Position = new Vector2(input.Position.X, input.Position.Y)
        };

        return content;
    }
}
