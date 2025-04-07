using System.Collections.Generic;
using BeeCreak.Scene;
using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.UI;

public class ButtonGroupController : IModelController<IEnumerable<Button>>
{
    private readonly ButtonGroupRenderer buttonRenderer;

    public ButtonGroupController(ButtonGroupRenderer buttonRenderer)
    {
        this.buttonRenderer = buttonRenderer;
    }

    private List<IDynamic> ButtonInteractions { get; set; } = new();

    private List<Button> Buttons { get; set; } = new();

    public void Load(IEnumerable<Button> buttons)
    {
        buttonRenderer.Load(buttons);

        foreach (var button in Buttons)
        {
            ButtonInteractions.Add(new ButtonInteraction(button));
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach (var buttonInteraction in ButtonInteractions)
        {
            buttonInteraction.Update(gameTime);
        }
    }

    public void Draw()
    {
        buttonRenderer.Draw();
    }
}