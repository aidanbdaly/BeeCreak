using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentImporter(BoundingBoxSheetConfig.FileExtension, DisplayName = BoundingBoxSheetConfig.ImporterDisplayName, DefaultProcessor = BoundingBoxSheetConfig.DefaultProcessor)]
public sealed class BoundingBoxSheetImporter : ContentImporter<BoundingBoxSheetDto>
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public override BoundingBoxSheetDto Import(string filename, ContentImporterContext context)
    {
        try
        {
            var json = File.ReadAllText(filename);
            var result = JsonSerializer.Deserialize<BoundingBoxSheetDto>(json, SerializerOptions);

            if (result is null)
            {
                throw new InvalidContentException($"Failed to deserialize 'BoundingBoxSheet' from '{filename}'.");
            }

            return result;
        }
        catch (Exception ex)
        {
            context.Logger.LogImportantMessage($"Error importing {filename}: {ex.Message}");
            throw;
        }
    }
}
