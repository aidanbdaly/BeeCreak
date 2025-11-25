using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentImporter(CellReferenceConfig.FileExtension, DisplayName = CellReferenceConfig.ImporterDisplayName, DefaultProcessor = CellReferenceConfig.DefaultProcessor)]
public sealed class CellReferenceImporter : ContentImporter<CellReferenceDto>
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public override CellReferenceDto Import(string filename, ContentImporterContext context)
    {
        try
        {
            var json = File.ReadAllText(filename);
            var result = JsonSerializer.Deserialize<CellReferenceDto>(json, SerializerOptions);

            if (result is null)
            {
                throw new InvalidContentException($"Failed to deserialize 'CellReference' from '{filename}'.");
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
