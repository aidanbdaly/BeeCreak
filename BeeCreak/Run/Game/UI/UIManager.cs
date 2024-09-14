using System.Collections.Generic;
using BeeCreak.Run.UI.Events;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.UI;

public class UIManager
{
    public List<Element> Elements { get; set; } = default!;
    private IEventManager EventBus { get; set; } = default!;
    private IToolCollection Tools { get; set; } = default!;

    public UIManager(IToolCollection context, IEventManager eventBus)
    {
        Tools = context;
        EventBus = eventBus;

        Elements = new List<Element> { new Clock(Tools), new Fps(Tools) };

        EventBus.Listen<AddUiElementEvent>(AddUiElement);
        EventBus.Listen<RemoveUiElementEvent>(RemoveUiElement);
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
