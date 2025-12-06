using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = EntityModelConfig.ProcessorDisplayName)]
public sealed class EntityModelProcessor : ContentProcessor<EntityModelDto, EntityModelContent>
{
    public override EntityModelContent Process(EntityModelDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new EntityModelContent
        {
Id = input.Id,
BoundingBoxSheet = input.BoundingBoxSheet,
};


if (input.Animations is not null)
        {
            foreach (var item in input.Animations)
            {
content.Animations.Add(item ?? string.Empty);
}
        }
if (input.Behaviours is not null)
        {
            foreach (var item in input.Behaviours)
            {
content.Behaviours.Add(item ?? string.Empty);
}
        }
return content;
    }

    private static void AssertValid(EntityModelDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("EntityModel payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("EntityModel requires ''.");
        }

        if (input.Animations is null || input.Animations.Count < 1)
        {
throw new InvalidContentException("EntityModel requires at least 1 '' entries.");
}

if (string.IsNullOrWhiteSpace(input.BoundingBoxSheet))
        {
            throw new InvalidContentException("EntityModel requires ''.");
        }

}
}
