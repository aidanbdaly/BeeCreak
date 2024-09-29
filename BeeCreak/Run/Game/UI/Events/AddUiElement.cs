namespace BeeCreak.Run.Game.UI.Events;

public class AddUiElementEvent
{
    public Element Element { get; set; }

    public AddUiElementEvent(Element element)
    {
        Element = element;
    }
}
