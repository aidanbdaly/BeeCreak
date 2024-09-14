using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeeCreak.Run;

public interface IToolCollection
{
    public IStaticToolCollection Static { get; set; }
    public IDynamicToolCollection Dynamic { get; set; }

    public interface IStaticToolCollection
    {
        public GraphicsDevice GraphicsDevice { get; set; }
        public Sprite Sprite { get; set; }
        public int TILE_SIZE { get; set; }
    }

    public interface IDynamicToolCollection : IDynamic
    {
        public Input Input { get; set; }
        public Sound Sound { get; set; }
        public Time Time { get; set; }
        public Camera Camera { get; set; }
    }
}
