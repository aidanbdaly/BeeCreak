using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "EntityReference Processor")]
public sealed class EntityReferenceProcessor : ContentProcessor<EntityReferenceDto, EntityReferenceContent>
{
    public override EntityReferenceContent Process(EntityReferenceDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new EntityReferenceContent
        {
Id = input.Id,
Entity = string.IsNullOrWhiteSpace(input.Entity) ? null : LoadAsset<EntityContent>(input.Entity, "Entity", "Entity", ".erec", "EntityProcessor", context),
Variant = input.Variant,
Position = MapPosition(input.Position, context),
};


return content;
    }

    private static void AssertValid(EntityReferenceDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("EntityReference payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("EntityReference requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.Variant))
        {
            throw new InvalidContentException("EntityReference requires ''.");
        }

}

private static EntityReferenceContent.PositionContent MapPosition(EntityReferenceDto.PositionDto? input, ContentProcessorContext context)
    {
        if (input is null)
        {
            return new EntityReferenceContent.PositionContent();
        }

        var content = new EntityReferenceContent.PositionContent();
content.X = input.X;
content.Y = input.Y;
return content;
    }

private static TContent LoadAsset<TContent>(
        string assetId,
        string assetName,
        string directory,
        string extension,
        string processor,
        ContentProcessorContext context)
    {
        if (string.IsNullOrWhiteSpace(assetId))
        {
            throw new InvalidContentException($"{assetName} reference is empty.");
        }

        var assetPath = string.Concat(directory, "/", assetId, extension);
        var reference = new ExternalReference<TContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<TContent, TContent>(reference, processor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"{assetName} '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
