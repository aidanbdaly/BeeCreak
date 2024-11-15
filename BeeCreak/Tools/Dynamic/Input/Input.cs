namespace BeeCreak.Tools.Dynamic.Input
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class Input : IInput
    {
        private KeyboardState previousState;
        private GamePadState previousGamePadState;

        public Input()
        {
            previousState = Keyboard.GetState();
        }

        public bool OnActionHold(InputMap action)
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
                && previousState.IsKeyUp(action.Keys.Value))
            {
                return true;
            }

            if (
                action.Buttons != null
                && gamePadState.IsButtonDown(action.Buttons.Value)
                && previousGamePadState.IsButtonUp(action.Buttons.Value))
            {
                return true;
            }

            return false;
        }

        public void Update(GameTime gameTime)
        {
            previousState = Keyboard.GetState();
            previousGamePadState = GamePad.GetState(PlayerIndex.One);
        }
    }
}