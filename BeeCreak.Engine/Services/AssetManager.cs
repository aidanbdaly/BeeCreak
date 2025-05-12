using Microsoft.Xna.Framework.Content;

namespace BeeCreak.Shared.Services;

public class AssetManager
{
    private readonly Dictionary<string, object> assets = new();

    public void Load<T>(ContentManager content, string source)
    {
        foreach (var file in Directory.EnumerateFiles(source))
        {
            var name = Path.GetRelativePath(content.RootDirectory, Path.ChangeExtension(file, null))
                    .Replace('\\', '/');

            try
            {
                Console.WriteLine(name);
                var asset = content.Load<T>(name);
                Console.WriteLine($"Loaded asset: {name}");
                assets[name] = asset;
            }
            catch (ContentLoadException)
            {
                Console.WriteLine($"Failed to load asset: {name}");
                // Log or skip failed asset
                continue;
            }
        }
    }

    public T Get<T>(string name)
    {
        if (!assets.TryGetValue(name, out var asset))
        {
            throw new KeyNotFoundException($"Asset '{name}' not found.");
        }

        if (asset is T typedAsset)
        {
            return typedAsset;
        }

        throw new InvalidCastException($"Asset '{name}' is not of type {typeof(T).Name}.");
    }
}