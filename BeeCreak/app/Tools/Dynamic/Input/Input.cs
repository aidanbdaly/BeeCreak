namespace BeeCreak.Tools.Dynamic.Input
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class Input : IInput
    {
        private KeyboardState previousState;

        private GamePadState previousGamePadState;

        private KeyboardState currentState;

        private GamePadState currentGamePadState;

        public Input()
        {
            currentState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            previousState = currentState;
            previousGamePadState = currentGamePadState;
        }

        public bool OnActionHold(InputMap action)
        {
            if (action.Keys != null && currentState.IsKeyDown(action.Keys.Value))
            {
                Console.WriteLine("Key is down" + action.Keys.Value);
                return true;
            }

            if (action.Buttons != null && currentGamePadState.IsButtonDown(action.Buttons.Value))
            {
                return true;
            }

            return false;
        }

        public bool OnActionClick(InputMap action)
        {
            if (
                action.Keys != null
                && currentState.IsKeyDown(action.Keys.Value)
                && previousState.IsKeyUp(action.Keys.Value))
            {
                return true;
            }

            if (
                action.Buttons != null
                && currentGamePadState.IsButtonDown(action.Buttons.Value)
                && previousGamePadState.IsButtonUp(action.Buttons.Value))
            {
                return true;
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            Console.WriteLine("Updating input");
            previousState = currentState;
            previousGamePadState = currentGamePadState;

            currentState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }
    }
}