using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Tools.Static;

public class Sprite : ISprite
{
    public Sprite(ContentManager contentManager, BeeCreak game)
    {
        Textures = GetAvailable<Texture2D>(contentManager, "png");
        Fonts = GetAvailable<SpriteFont>(contentManager, "spritefont");

        Batch = new SpriteBatch(game.GraphicsDevice);
        GraphicsDevice = game.GraphicsDevice;
    }

    public SpriteBatch Batch { get; set; }

    public GraphicsDevice GraphicsDevice { get; set; }

    private Dictionary<string, Texture2D> Textures { get; set; }

    private Dictionary<string, SpriteFont> Fonts { get; set; }

    public Texture2D GetTexture(string textureName)
    {
        return Textures[textureName];
    }

    public SpriteFont GetFont(string fontName)
    {
        return Fonts[fontName];
    }

    public Rectangle GetBounds(string textureName)
    {
        return new Rectangle(0, 0, Textures[textureName].Width, Textures[textureName].Height);
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
