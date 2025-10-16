using BeeCreak.Engine.Components;

public class MenuComponent(IEnumerable<ButtonComponent> buttons, int gap) : ComponentArray<ButtonComponent>(gap, buttons) { }