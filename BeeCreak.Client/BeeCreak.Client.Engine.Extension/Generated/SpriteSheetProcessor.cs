using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "SpriteSheet Processor")]
public sealed class SpriteSheetProcessor : ContentProcessor<SpriteSheetDto, SpriteSheetContent>
{
    public override SpriteSheetContent Process(SpriteSheetDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new SpriteSheetContent
        {
Id = input.Id,
Texture = string.IsNullOrWhiteSpace(input.Texture) ? null : LoadAsset<TextureContent>(input.Texture, "Texture", "Image", ".png", "TextureProcessor", context),
};


if (input.Data is not null)
        {
            foreach (var entry in input.Data)
            {
content.Data[entry.Key] = MapDataEntry(entry.Value, context);
}
        }
return content;
    }

    private static void AssertValid(SpriteSheetDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("SpriteSheet payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("SpriteSheet requires ''.");
        }

        if (input.Data is null || input.Data.Count < 1)
        {
throw new InvalidContentException("SpriteSheet requires at least 1 '' entries.");
}
}

private static SpriteSheetContent.DataEntryContent MapDataEntry(SpriteSheetDto.DataEntryDto? input, ContentProcessorContext context)
    {
        if (input is null)
        {
            return new SpriteSheetContent.DataEntryContent();
        }

        var content = new SpriteSheetContent.DataEntryContent();
content.X = input.X;
content.Y = input.Y;
content.W = input.W;
content.H = input.H;
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
