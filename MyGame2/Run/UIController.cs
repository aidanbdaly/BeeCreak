namespace BeeCreak.Run;

using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class UIController
{
    public List<Element> Elements { get; set; } = default!;
    public IContext Context { get; set; } = default!;
    public Vector2 ScreenDimensions { get; set; }

    public UIController(IContext context)
    {
        Context = context;

        ScreenDimensions = new Vector2(
            Context.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Width,
            Context.Static.GraphicsDevice.Adapter.CurrentDisplayMode.Height
        );

        Elements = new List<Element> { new Clock(Context), };
    }

    public void Update(GameTime gameTime)
    {
        foreach (var element in Elements)
        {
            element.Update(gameTime);
        }
    }

    public void Draw()
    {
        foreach (var element in Elements)
        {
            element.Draw();
        }
    }
}
