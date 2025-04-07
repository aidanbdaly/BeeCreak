using System;
using BeeCreak.App;
using BeeCreak.Shared.Data.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Shared.UI;

public class ButtonInteraction : IDynamic
{
    private readonly Button button;

    public ButtonInteraction(Button button)
    {
        this.button = button;
    }

    private MouseState PreviousState { get; set; }

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        var mousePosition = new Vector2(mouseState.X, mouseState.Y);

        var hovered = mousePosition.X > button.Position.X - (button.Bounds.Width / 2)
            && mousePosition.X < button.Position.X + (button.Bounds.Width / 2)
            && mousePosition.Y > button.Position.Y - (button.Bounds.Height / 2)
            && mousePosition.Y < button.Position.Y + (button.Bounds.Height / 2);

        if (
            hovered
            && mouseState.LeftButton == ButtonState.Released
            && PreviousState.LeftButton == ButtonState.Pressed)
        {
            try
            {
                ActionRegistry.TryGetAction(button.Action, out var action);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Failed to invoke button action: " + button.Action + " is not registered.");
            }

            if (hovered)
            {
                button.State = ButtonComponentState.hovered;
            }
            else
            {
                button.State = ButtonComponentState.Idle;
            }

            PreviousState = mouseState;
        }
    }
}