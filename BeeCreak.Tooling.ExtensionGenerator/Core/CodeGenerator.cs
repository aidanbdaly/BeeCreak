using System;
using System.Collections.Generic;
using System.Linq;
using Scriban;

namespace BeeCreak.ExtensionGenerator
{
    public static class CodeGenerator
    {
        public static async Task GenerateAsync(
            IReadOnlyList<AssetType> assets,
            string templatesDir,
            string outputDir,
            string @namespace)
        {
            Directory.CreateDirectory(outputDir);

            var contentTemplate = await LoadTemplateAsync(Path.Combine(templatesDir, "content.sbn"));
            var dtoTemplate = await LoadTemplateAsync(Path.Combine(templatesDir, "dto.sbn"));
            var importerTemplate = await LoadTemplateAsync(Path.Combine(templatesDir, "importer.sbn"));
            var processorTemplate = await LoadTemplateAsync(Path.Combine(templatesDir, "processor.sbn"));
            var writerTemplate = await LoadTemplateAsync(Path.Combine(templatesDir, "writer.sbn"));

            foreach (var asset in assets)
            {
                var ctx = new
                {
                    asset,
                    @namespace,
                    required_namespaces = CollectNamespaces(asset),
                    runtime_reader_full_name = $"{asset.RuntimeNamespace}.Readers.{asset.Name}Reader, {asset.RuntimeAssembly}",
                    runtime_type_full_name = $"{asset.RuntimeNamespace}.Models.{asset.Name}, {asset.RuntimeAssembly}"
                };

                await WriteFileAsync(outputDir, $"{asset.Name}Dto.cs", dtoTemplate, ctx);
                await WriteFileAsync(outputDir, $"{asset.Name}Content.cs", contentTemplate, ctx);
                await WriteFileAsync(outputDir, $"{asset.Name}Writer.cs", writerTemplate, ctx);
                await WriteFileAsync(outputDir, $"{asset.Name}Processor.cs", processorTemplate, ctx);
                await WriteFileAsync(outputDir, $"{asset.Name}Importer.cs", importerTemplate, ctx);
            }
        }

        private static async Task<Template> LoadTemplateAsync(string path)
        {
            var text = await File.ReadAllTextAsync(path);
            return Template.Parse(text, path);
        }

        private static async Task WriteFileAsync(
            string outputDir,
            string fileName,
            Template template,
            object model)
        {
            var rendered = await template.RenderAsync(model, memberRenamer: member => member.Name.ToLowerInvariant());
            var fullPath = Path.Combine(outputDir, fileName);
            await File.WriteAllTextAsync(fullPath, rendered);
            Console.WriteLine($"Wrote {fullPath}");
        }

        private static IReadOnlyCollection<string> CollectNamespaces(AssetType asset)
        {
            return asset.Properties
                .SelectMany(p =>
                    (p.ReferenceNamespaces ?? Array.Empty<string>()).Concat(
                    p.ElementReferenceNamespaces ?? Array.Empty<string>()))
                .Where(ns => !string.IsNullOrWhiteSpace(ns))
                .Distinct(StringComparer.Ordinal)
                .ToArray();
        }
    }
}
