using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.Entity.Model;

[ContentProcessor(DisplayName = EntityModelConfig.ProcessorDisplayName)]
public sealed class EntityModelProcessor : ContentProcessor<EntityModelDTO, EntityModelContent>
{
    public override EntityModelContent Process(EntityModelDTO input, ContentProcessorContext context)
    {
        AssertValid(input);

        var animationCollection = BuildAnimationCollection(input.Animations, context);
        var boundingBoxSheet = BuildBoundingBoxSheet(input.BoundingBoxSheet, context);

        var content = new EntityModelContent
        {
            Id = input.Id,
            Animations = animationCollection,
            BoundingBoxSheet = boundingBoxSheet
        };

        if (input.Behaviours is not null)
        {
            foreach (var behaviour in input.Behaviours)
            {
                if (!string.IsNullOrWhiteSpace(behaviour))
                {
                    content.Behaviours.Add(behaviour);
                }
            }
        }

        return content;
    }

    private static void AssertValid(EntityModelDTO input)
    {
        if (input is null)
        {
            throw new InvalidContentException("Entity record payload is empty.");
        }

        if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("Entity record requires an id.");
        }

        if (input.Animations is null || input.Animations.Count == 0)
        {
            throw new InvalidContentException($"Entity record '{input.Id}' requires at least one animation.");
        }

        if (string.IsNullOrWhiteSpace(input.BoundingBoxSheet))
        {
            throw new InvalidContentException($"Entity record '{input.Id}' requires a boundingBoxSheet id.");
        }
    }
}
