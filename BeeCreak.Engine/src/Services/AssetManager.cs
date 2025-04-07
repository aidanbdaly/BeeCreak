using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace BeeCreak.Shared.Services;

public static class AssetManager
{
    private static readonly Dictionary<Type, Dictionary<string, object>> assets = new();

    public static void LoadAll(ContentManager content)
    {
        Load<SpriteSheet>(content, "../../Content/src/Spritesheet");
        Load<Animation>(content, "../../Content/src/Animations");
        Load<Texture2D>(content, "../../Content/src/Images");
        Load<Song>(content, "../../Content/src/Sounds");
    }

    private static void Load<T>(ContentManager content, string source)
    {
        var assetDictionary = new Dictionary<string, object>();

        foreach (var file in Directory.EnumerateFiles(source))
        {
            var name = Path.GetFileNameWithoutExtension(file);

            try
            {
                assetDictionary[name] = content.Load<T>(name);
            }
            catch (ContentLoadException error)
            {
                continue;
            }
        }

        assets[typeof(T)] = assetDictionary;
    }

    public static T Get<T>(string name)
    {
        return (T)assets[typeof(T)][name];
    }
}