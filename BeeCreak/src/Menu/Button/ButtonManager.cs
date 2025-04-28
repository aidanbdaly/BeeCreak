using BeeCreak.Shared.UI;

public class ButtonManager
{
    public List<Button> Buttons { get; private set; } = new List<Button>();

    public void AddButton(Button button)
    {
        Buttons.Add(button);
    }

    public void RemoveButton(Button button)
    {
        Buttons.Remove(button);
    }
}