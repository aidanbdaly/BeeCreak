using BeeCreak.Engine.Presentation.Composition;
using BeeCreak.Engine.Presentation.UI.Input;

public class MenuComponent(IEnumerable<ButtonComponent> buttons, int gap) : ComponentArray<ButtonComponent>(gap, buttons) { }