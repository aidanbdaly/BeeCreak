using System.Collections.Generic;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.Game.Scene.Light;

public class LightManager
{
    public List<Light> Lights { get; set; }
    public bool RenderLighting { get; set; } = true;
    private Texture2D LightMap { get; set; }
    private RenderTarget2D LightTarget { get; set; }
    private IToolCollection Tools { get; set; }

    public LightManager(IToolCollection tools, List<Light> lights, int size)
    {
        Tools = tools;
        Lights = lights;

        var sizeInPixels = size * tools.Static.TILE_SIZE;

        LightMap = new Texture2D(tools.Static.GraphicsDevice, size, size);
        LightTarget = new RenderTarget2D(tools.Static.GraphicsDevice, sizeInPixels, sizeInPixels);
    }

    public void Update(GameTime gameTime)
    {
        if (Tools.Dynamic.Input.OnKeyClick(Keys.L))
        {
            RenderLighting = !RenderLighting;
        }

        foreach (var light in Lights)
        {
            light.Update(gameTime);
        }
    }

    public void Draw()
    {
        foreach (var light in Lights)
        {
            light.Draw();
        }
    }
}
