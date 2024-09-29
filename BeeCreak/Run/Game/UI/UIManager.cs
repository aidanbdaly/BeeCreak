using System.Collections.Generic;
using BeeCreak.Run.Game.Objects.Time;
using BeeCreak.Run.Game.UI.Events;
using BeeCreak.Run.Game.UI.Instances;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.Game.UI;

public class UIManager
{
    public List<Element> Elements { get; set; }
    private ITime Time { get; set; }
    private IToolCollection Tools { get; set; }

    public UIManager(IToolCollection context)
    {
        Tools = context;

        Elements = new List<Element> { new Clock(Tools, Time), new Fps(Tools) };
    }

    private void RemoveUiElement(RemoveUiElementEvent e)
    {
        Elements.Remove(e.Element);
    }

    private void AddUiElement(AddUiElementEvent e)
    {
        Elements.Add(e.Element);
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
