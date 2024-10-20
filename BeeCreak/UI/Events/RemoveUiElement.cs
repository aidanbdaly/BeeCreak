namespace BeeCreak.Game.UI.Events;

public class RemoveUiElementEvent
{
    public Element Element { get; set; }

    public RemoveUiElementEvent(Element element)
    {
        Element = element;
    }
}
