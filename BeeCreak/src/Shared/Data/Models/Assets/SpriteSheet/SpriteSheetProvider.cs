using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Shared.Data.Models;

public class SpriteSheetProvider : ISpriteSheetProvider
{
    public SpriteSheetProvider()
    {
    }

    private Dictionary<string, Texture2D> SpriteSheets { get; set; }

    public Texture2D GetSpriteSheet(string spriteSheetName)
    {
        return SpriteSheets[spriteSheetName];
    }

    public void Load(ContentManager contentManager)
    {
        var spriteSheetNames = new List<string>
        {
            "textures/buttons",
            "textures/tiles",
            "textures/entities"
        };

        SpriteSheets = new Dictionary<string, Texture2D>();

        foreach (var spriteSheetName in spriteSheetNames)
        {
            SpriteSheets[spriteSheetName] = contentManager.Load<Texture2D>(spriteSheetName);
        }
    }
}