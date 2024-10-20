using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Tools;

public class Input : IDynamic
{
    public KeyboardState PreviousState;
    public GamePadState PreviousGamePadState;

    public Input()
    {
        PreviousState = Keyboard.GetState();
    }

    public static bool OnActionHold(InputMap action)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

        if (action.Keys != null && keyboardState.IsKeyDown(action.Keys.Value))
        {
            return true;
        }

        if (action.Buttons != null && gamePadState.IsButtonDown(action.Buttons.Value))
        {
            return true;
        }

        return false;
    }

    public bool OnActionClick(InputMap action)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

        if (
            action.Keys != null
            && keyboardState.IsKeyDown(action.Keys.Value)
            && PreviousState.IsKeyUp(action.Keys.Value)
        )
        {
            return true;
        }

        if (
            action.Buttons != null
            && gamePadState.IsButtonDown(action.Buttons.Value)
            && PreviousGamePadState.IsButtonUp(action.Buttons.Value)
        )
        {
            return true;
        }

        return false;
    }

    public void Update(GameTime gameTime)
    {
        PreviousState = Keyboard.GetState();
        PreviousGamePadState = GamePad.GetState(PlayerIndex.One);
    }
}
