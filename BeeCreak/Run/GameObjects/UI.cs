using System.Collections.Generic;
using BeeCreak.Run.GameObjects;
using BeeCreak.Run.GameObjects.Instances;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run;

public class UI : IGameObject
{
    private List<Element> Elements { get; set; } = default!;
    private IToolCollection Tools { get; set; } = default!;

    public UI(IToolCollection context)
    {
        Tools = context;

        Elements = new List<Element> { new Clock(Tools), new Fps(Tools) };
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
