using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class ButtonInteractionSystem
{
    private readonly ButtonManager buttonManager;

    public ButtonInteractionSystem(ButtonManager buttonManager)
    {
        this.buttonManager = buttonManager;
    }

    private MouseState lastMouseState;

    public void Update(GameTime gameTime)
    {
        var mouseState = Mouse.GetState();

        foreach (var button in buttonManager.Buttons)
        {
            if (button.Status == ButtonStatus.Disabled)
            {
                continue;
            }

            var absoluteBounds = new Rectangle(
                (int)button.Position.X,
                (int)button.Position.Y,
                button.Bounds.Width,
                button.Bounds.Height
            );

            if (absoluteBounds.Contains(mouseState.Position))
            {
                if (button.Status != ButtonStatus.Hovered)
                {
                    button.Status = ButtonStatus.Hovered;
                    button.DispatchOnHoverEvent();
                }

                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    if (button.Status != ButtonStatus.Pressed)
                    {
                        button.Status = ButtonStatus.Pressed;
                        button.DispatchOnClickEvent();
                    }
                }
            }
        }
    }
}