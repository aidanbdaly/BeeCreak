using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Engine.Input
{
    static class InputManager
    {
        public static MouseState CurrentMouseState { get; private set; } = Mouse.GetState();

        public static MouseState LastMouseState { get; private set; } = Mouse.GetState();

        public static KeyboardState CurrentKeyboardState { get; private set; } = Keyboard.GetState();

        public static KeyboardState LastKeyboardState { get; private set; } = Keyboard.GetState();

        public static void Update()
        {
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();
        }

        public static bool IsMouseClicked()
        {
            return CurrentMouseState.LeftButton == ButtonState.Pressed &&
                   LastMouseState.LeftButton == ButtonState.Released;
        }
    }
}