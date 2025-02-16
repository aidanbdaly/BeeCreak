using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Data.Models;

public class Sprite
{
    public Sprite() { }

    public string Name { get; set; }

    public Rectangle SourceRectangle { get; set; } = new();
}