using System;
using Microsoft.Xna.Framework.Content.Pipeline;
namespace BeeCreak.Extension.Generated;

[ContentProcessor(DisplayName = "TileMap Processor")]
public sealed class TileMapProcessor : ContentProcessor<TileMapDto, TileMapContent>
{
    public override TileMapContent Process(TileMapDto input, ContentProcessorContext context)
    {
        AssertValid(input);

var content = new TileMapContent
        {
Id = input.Id,
SpriteSheet = string.IsNullOrWhiteSpace(input.SpriteSheet) ? null : LoadAsset<SpriteSheetContent>(input.SpriteSheet, "SpriteSheet", "SpriteSheet", ".spritesheet", "SpriteSheetProcessor", context),
BoundingBoxSheet = string.IsNullOrWhiteSpace(input.BoundingBoxSheet) ? null : LoadAsset<BoundingBoxSheetContent>(input.BoundingBoxSheet, "BoundingBoxSheet", "BoundingBoxSheet", ".bbs", "BoundingBoxSheetProcessor", context),
};


if (input.Data is not null)
        {
            foreach (var item in input.Data)
            {
content.Data.Add(item);
}
        }
return content;
    }

    private static void AssertValid(TileMapDto input)
    {
        if (input is null)
        {
            throw new InvalidContentException("TileMap payload is empty.");
        }

if (string.IsNullOrWhiteSpace(input.Id))
        {
            throw new InvalidContentException("TileMap requires ''.");
        }

if (string.IsNullOrWhiteSpace(input.BoundingBoxSheet))
        {
            throw new InvalidContentException("TileMap requires ''.");
        }

        if (input.Data is null || input.Data.Count < 1)
        {
throw new InvalidContentException("TileMap requires at least 1 '' entries.");
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
