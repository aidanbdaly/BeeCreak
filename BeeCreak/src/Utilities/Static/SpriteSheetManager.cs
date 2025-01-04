using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Utilities.Static;

public class SpriteSheetManager : ISpriteSheetManager
{
    public SpriteSheetManager()
    {
    }

    private Dictionary<string, Texture2D> SpriteSheets { get; set; }

    public Texture2D GetSpriteSheet(string spriteSheet)
    {
        return SpriteSheets[spriteSheet];
    }

    public void LoadSpriteSheets(ContentManager content)
    {
        var dict = new Dictionary<string, Texture2D>();

        var contentDirectory = "Content";

        var files = Directory
            .GetFiles(contentDirectory, "*.png", SearchOption.AllDirectories)
            .ToList();

        foreach (var file in files)
        {
            string relativePath = file.Substring(contentDirectory.Length + 1);
            string name = Path.ChangeExtension(relativePath, null);

            dict.Add(name, content.Load<Texture2D>(name));
        }

        SpriteSheets = dict;
    }
}