using System;
using System.IO;
using BeeCreak.ExtensionGenerator;
using NJsonSchema;

class Program
{
    static async Task Main(string[] args)
    {
        var hostDirectory = Directory.GetCurrentDirectory();

        var schemaPath = ResolvePath(args, 0, hostDirectory, "schema.json");
        var outputDir = ResolvePath(args, 1, hostDirectory, "Generated");

        var ns = args.Length > 2 ? args[2] : "BeeCreak.Extension.Generated";

        var templatesDir = LocateTemplatesDirectory();

        var schema = await JsonSchema.FromFileAsync(schemaPath);
        var assets = AssetModelBuilder.Build(schema);

        await CodeGenerator.GenerateAsync(assets, templatesDir, outputDir, ns);
    }

    private static string ResolvePath(string[] args, int index, string baseDir, string defaultRelativePath)
    {
        string path;
        if (args.Length > index && !string.IsNullOrWhiteSpace(args[index]))
        {
            path = args[index];
        }
        else
        {
            path = defaultRelativePath;
        }

        return Path.GetFullPath(Path.IsPathRooted(path) ? path : Path.Combine(baseDir, path));
    }

    private static string LocateTemplatesDirectory()
    {
        var baseDir = AppContext.BaseDirectory;
        var candidate = Path.Combine(baseDir, "Templates");
        if (Directory.Exists(candidate))
        {
            return candidate;
        }

        return Path.Combine(baseDir, "..", "..", "..", "Templates");
    }
}
