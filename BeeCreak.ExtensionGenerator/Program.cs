using BeeCreak.ExtensionGenerator;
using NJsonSchema;

class Program
{
    static async Task Main(string[] args)
    {
        var schemaPath = args.Length > 0 ? args[0] : "schema.json";
        var templatesDir = "Templates";
        var outputDir = "Output";
        var ns = "BeeCreak.Extension.Test";

        var schema = await JsonSchema.FromFileAsync(schemaPath);
        var assets = AssetModelBuilder.Build(schema); 

        await CodeGenerator.GenerateAsync(assets, templatesDir, outputDir, ns);
    }
}