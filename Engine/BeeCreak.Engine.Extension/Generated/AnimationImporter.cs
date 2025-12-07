using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extension.Generated;

[ContentImporter(".as", DisplayName = "Animation Importer", DefaultProcessor = "AnimationProcessor")]
public sealed class AnimationImporter : ContentImporter<AnimationDto>
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };

    public override AnimationDto Import(string filename, ContentImporterContext context)
    {
        try
        {
            var json = File.ReadAllText(filename);
            var result = JsonSerializer.Deserialize<AnimationDto>(json, SerializerOptions);

            if (result is null)
            {
                throw new InvalidContentException($"Failed to deserialize 'Animation' from '{filename}'.");
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
