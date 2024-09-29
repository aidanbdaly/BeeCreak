using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run.Tools.Static;

public class StaticTools : IStaticToolCollection
{
    public GraphicsDevice GraphicsDevice { get; set; }
    public Sprite Sprite { get; set; }
    public EventManager Events { get; set; }
    public int TILE_SIZE { get; set; }
}
