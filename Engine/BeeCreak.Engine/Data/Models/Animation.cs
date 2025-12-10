using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Engine.Data.Models
{
    public record Animation
    (
        string Id,
        Texture2D Texture,
        List<Rectangle> Data
    );
}