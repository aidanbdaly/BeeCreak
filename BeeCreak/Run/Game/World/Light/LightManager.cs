using BeeCreak.Run.GameObjects.World.Light.Delegates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.World.Light;

public class LightManager
{
    public ICell Cell { get; set; }
    public RenderTarget2D LightTarget { get; set; }
    public bool RenderLighting { get; set; } = true;
    private Texture2D LightMap { get; set; }
    private IsOpaqueDelegate IsOpaque { get; set; }
    private IToolCollection Tools { get; set; }

    public LightManager(IToolCollection tools, IsOpaqueDelegate isOpaque, ICell cell)
    {
        IsOpaque = isOpaque;
        Tools = tools;
        Cell = cell;

        var sizeInPixels = cell.Size * tools.Static.TILE_SIZE;

        LightMap = new Texture2D(tools.Static.GraphicsDevice, cell.Size, cell.Size);
        LightTarget = new RenderTarget2D(tools.Static.GraphicsDevice, sizeInPixels, sizeInPixels);
    }

    public void Update(GameTime gameTime)
    {
        if (Tools.Dynamic.Input.OnKeyClick(Keys.L))
        {
            RenderLighting = !RenderLighting;
        }

        foreach (var light in Cell.Lights)
        {
            light.Update(gameTime);
        }
    }

    public void Draw()
    {
        foreach (var light in Cell.Lights)
        {
            light.Draw();
        }
    }
}
