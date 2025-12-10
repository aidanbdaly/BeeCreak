using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "AnimationCollection Processor")]
public sealed class AnimationCollectionProcessor : ContentProcessor<AnimationCollectionDto, AnimationCollectionContent>
{
    public override AnimationCollectionContent Process(AnimationCollectionDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new AnimationCollectionContent
        {
Id = input.Id,
};


if (input.Data is not null)
        {
            foreach (var entry in input.Data)
            {
if (string.IsNullOrWhiteSpace(entry.Value))
                {
                    continue;
                }
content.Data[entry.Key] = LoadAsset<AnimationContent>(entry.Value, "Animation", "Animation", ".as", "AnimationProcessor", context);
}
        }
return content;
    }

    private static void AssertValid(AnimationCollectionDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("AnimationCollection payload is empty.");
        }

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
