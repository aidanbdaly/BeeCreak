using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

public static class CellRecordLoader
{
    public static CellRecordContent Load(string assetId, ContentProcessorContext context)
    {
        var assetPath = string.Concat(CellRecordConfig.ContentDirectory, "/", assetId, CellRecordConfig.FileExtension);
        var reference = new ExternalReference<CellRecordContent>(assetPath);

        try
        {
            return context.BuildAndLoadAsset<CellRecordContent, CellRecordContent>(reference, CellRecordConfig.DefaultProcessor);
        }
        catch (Exception ex)
        {
            throw new InvalidContentException($"CellRecord '{assetId}' failed to load: {ex.Message}", ex);
        }
    }
}
