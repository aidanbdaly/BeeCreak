using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Tools.Static;

public interface IStaticToolCollection
{
    GraphicsDevice GraphicsDevice { get; set; }
    Sprite Sprite { get; set; }
    EventManager Events { get; set; }
    int TILE_SIZE { get; set; }
}
