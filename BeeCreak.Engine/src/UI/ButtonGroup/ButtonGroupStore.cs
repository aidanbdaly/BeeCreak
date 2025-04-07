using System.Collections.Generic;
using BeeCreak.Shared.UI;

namespace BeeCreak.UI;

public class ButtonGroupStore 
{
    private readonly Dictionary<ButtonGroupType, IEnumerable<Button>> buttonGroups = new();

    public ButtonGroupStore() {}

    public IEnumerable<Button> Get(ButtonGroupType buttonGroupType)
    {
        return buttonGroups[buttonGroupType];
    }
}