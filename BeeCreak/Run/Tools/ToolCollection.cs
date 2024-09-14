using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static BeeCreak.Run.IToolCollection;

namespace BeeCreak.Run;

public class ToolCollection : IToolCollection
{
    public IStaticToolCollection Static { get; set; } = new StaticTools();
    public IDynamicToolCollection Dynamic { get; set; } = new DynamicTools();

    public class StaticTools : IStaticToolCollection
    {
        public GraphicsDevice GraphicsDevice { get; set; } = default!;
        public Sprite Sprite { get; set; } = default!;
        public int TILE_SIZE { get; set; } = default!;
    }

    public class DynamicTools : IDynamicToolCollection
    {
        public Input Input { get; set; }
        public Sound Sound { get; set; }
        public Time Time { get; set; }
        public Camera Camera { get; set; }

        public void Update(GameTime gameTime)
        {
            Input.Update(gameTime);
            Sound.Update(gameTime);
            Time.Update(gameTime);
            Camera.Update(gameTime);
        }
    }
}
