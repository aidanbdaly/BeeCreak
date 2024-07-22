namespace BeeCreak.Run;

using Microsoft.Xna.Framework.Graphics;
public class StaticContext
{
    public GraphicsDevice GraphicsDevice { get; set; } = default!;
    public SpriteController SpriteController { get; set; } = default!;
    public bool EnableDeveloperMode { get; set; }
    public int TILE_SIZE { get; set; } = default!;
}