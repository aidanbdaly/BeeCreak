using Microsoft.Xna.Framework.Graphics;

public class TextureAttribute
{
    public Texture2D Texture { get; set; }

    public string Name { get; set; }

    public TextureAttribute(Texture2D texture, string name)
    {
        Texture = texture;
        Name = name;
    }
}