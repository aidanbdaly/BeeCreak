namespace BeeCreak.Run;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class SpriteController
{
    public Dictionary<string, Texture2D> Textures { get; set; } = default!;
    public Dictionary<string, SpriteFont> Fonts { get; set; } = default!;
    public SpriteBatch Batch { get; set; } = default!;
    public BlendState Multiply =
        new()
        {
            ColorSourceBlend = Blend.DestinationColor,
            ColorDestinationBlend = Blend.Zero,
            ColorBlendFunction = BlendFunction.Add,
            AlphaSourceBlend = Blend.One,
            AlphaDestinationBlend = Blend.Zero,
            AlphaBlendFunction = BlendFunction.Add
        };

    public SpriteController(ContentManager contentManager, GraphicsDevice graphicsDevice)
    {
        Textures = GetAvailable<Texture2D>(contentManager, "png");
        Fonts = GetAvailable<SpriteFont>(contentManager, "spritefont");

        Batch = new SpriteBatch(graphicsDevice);
    }

    public Texture2D GetTexture(string textureName)
    {
        return Textures[textureName];
    }

    public SpriteFont GetFont(string fontName)
    {
        return Fonts[fontName];
    }

    private static Dictionary<string, T> GetAvailable<T>(ContentManager content, string extension)
    {
        var dict = new Dictionary<string, T>();

        var contentDirectory = "Content";

        var files = Directory
            .GetFiles(contentDirectory, $"*.{extension}", SearchOption.AllDirectories)
            .ToList();

        foreach (var file in files)
        {
            string relativePath = file.Substring(contentDirectory.Length + 1);
            string name = Path.ChangeExtension(relativePath, null);

            dict.Add(name, content.Load<T>(name));
        }

        return dict;
    }
}