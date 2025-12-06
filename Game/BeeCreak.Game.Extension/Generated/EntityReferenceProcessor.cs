using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = EntityReferenceConfig.ProcessorDisplayName)]
public sealed class EntityReferenceProcessor : ContentProcessor<EntityReferenceDto, EntityReferenceContent>
{
    public override EntityReferenceContent Process(EntityReferenceDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new EntityReferenceContent
        {
Id = input.Id,
Base = input.Base,
Variant = input.Variant,
Position = input.Position,
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

if (string.IsNullOrWhiteSpace(input.Base))
        {
            throw new InvalidContentException("EntityReference requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.Variant))
        {
            throw new InvalidContentException("EntityReference requires ''.");
        }

}
}
