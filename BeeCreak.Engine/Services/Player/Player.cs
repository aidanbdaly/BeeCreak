namespace BeeCreak.Shared.Services.Dynamic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class Player
{
    private KeyboardState previousState;

    private GamePadState previousGamePadState;

    private KeyboardState currentState;

    private GamePadState currentGamePadState;

    public Player() {}

    public bool OnAction(PlayerAction action)
    {
        currentState = Keyboard.GetState();
        currentGamePadState = GamePad.GetState(PlayerIndex.One);

        if (currentState.IsKeyDown(action.Key))
        {
            return true;
        }

        if (currentGamePadState.IsButtonDown(action.Button))
        {
            return true;
        }

        return false;
    }

    public bool OnActionCycle(PlayerAction action)
    {
        currentState = Keyboard.GetState();
        currentGamePadState = GamePad.GetState(PlayerIndex.One);

        if (
            currentState.IsKeyUp(action.Key)
            && previousState.IsKeyDown(action.Key))
        {
            return true;
        }

        if (
            currentGamePadState.IsButtonUp(action.Button)
            && previousGamePadState.IsButtonDown(action.Button))
        {
            return true;
        }

        return false;
    }

    public void Update(GameTime gameTime)
    {
        previousState = currentState;
        previousGamePadState = currentGamePadState;
    }
}