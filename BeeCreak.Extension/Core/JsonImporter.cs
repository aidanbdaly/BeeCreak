using System;
using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

namespace BeeCreak.Extensions.Core;

public class JsonImporter<T> : ContentImporter<T>
{
    public override T Import(string filename, ContentImporterContext context)
    {
        try
        {
            string json = File.ReadAllText(filename);
            T result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }
        catch (Exception ex)
        {
            context.Logger.LogImportantMessage($"Error importing {filename}: {ex.Message}");
            throw;
        }
    }
}
