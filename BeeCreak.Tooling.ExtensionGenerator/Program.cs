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
        var referenceDefinitions = new Dictionary<string, JsonSchema?>(StringComparer.OrdinalIgnoreCase);
        var builtinMetadata = new Dictionary<string, AssetReferenceMetadata>(StringComparer.OrdinalIgnoreCase)
        {
            ["Texture"] = new AssetReferenceMetadata(
                "Image",
                ".png",
                "TextureProcessor",
                new[] { "Microsoft.Xna.Framework.Content.Pipeline.Graphics" })
        };

        if (args.Length > 3)
        {
            foreach (var referenceArg in args[3..])
            {
                if (string.IsNullOrWhiteSpace(referenceArg))
                {
                    continue;
                }

                var referencePath = ResolvePath(new[] { referenceArg }, 0, hostDirectory, "");
                if (!File.Exists(referencePath))
                {
                    Console.WriteLine($"Reference schema '{referencePath}' not found; skipping.");
                    continue;
                }

                var referenceSchema = await JsonSchema.FromFileAsync(referencePath);
                foreach (var (referenceName, defSchema) in referenceSchema.Definitions)
                {
                    referenceDefinitions[referenceName] = defSchema;
                }
            }
        }

        var assets = AssetModelBuilder.Build(schema, referenceDefinitions, builtinMetadata);

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
