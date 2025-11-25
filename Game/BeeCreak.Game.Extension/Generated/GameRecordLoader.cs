using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class GameRecordLoader
{
    public static GameRecordContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(GameRecordConfig.ContentDirectory, "/", assetId, GameRecordConfig.FileExtension);
        var reference = new ExternalReference<GameRecordContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<GameRecordContent, GameRecordContent>(reference, GameRecordConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"GameRecord '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
