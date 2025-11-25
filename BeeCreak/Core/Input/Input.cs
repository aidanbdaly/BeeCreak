using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Core.Input
{
    public class InputManager(SceneManager sceneManager)
    {
        private KeyboardState previousState;

        private GamePadState previousGamePadState;

        private MouseState previousMouseState;

        private readonly SceneManager sceneManager = sceneManager;

        public bool ButtonDown(ButtonMap button)
        {
            var currentState = Keyboard.GetState();
            var currentGamePadState = GamePad.GetState(PlayerIndex.One);

            if (currentState.IsKeyDown(button.Key))
            {
                return true;
            }

            if (currentGamePadState.IsButtonDown(button.Button))
            {
                return true;
            }

            previousState = currentState;
            previousGamePadState = currentGamePadState;

            return false;
        }

        public bool ButtonCycled(ButtonMap button)
        {
            var currentState = Keyboard.GetState();
            var currentGamePadState = GamePad.GetState(PlayerIndex.One);

            if (
                currentState.IsKeyUp(button.Key)
                && previousState.IsKeyDown(button.Key))
            {
                return true;
            }

            if (
                currentGamePadState.IsButtonUp(button.Button)
                && previousGamePadState.IsButtonDown(button.Button))
            {
                return true;
            }

            previousState = currentState;
            previousGamePadState = currentGamePadState;

            return false;
        }

        public bool PointerButtonDown(PointerButtonMap button)
        {
            var currentState = Mouse.GetState();

            if (button.Button == PointerButton.Left && currentState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }

            if (button.Button == PointerButton.Right && currentState.RightButton == ButtonState.Pressed)
            {
                return true;
            }

            if (button.Button == PointerButton.Middle && currentState.MiddleButton == ButtonState.Pressed)
            {
                return true;
            }

            previousMouseState = currentState;

            return false;
        }

        public bool PointerButtonCycled(PointerButtonMap button)
        {
            var currentState = Mouse.GetState();

            if (
                button.Button == PointerButton.Left
                && currentState.LeftButton == ButtonState.Released
                && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }

            if (
                button.Button == PointerButton.Right
                && currentState.RightButton == ButtonState.Released
                && previousMouseState.RightButton == ButtonState.Pressed)
            {
                return true;
            }

            if (
                button.Button == PointerButton.Middle
                && currentState.MiddleButton == ButtonState.Released
                && previousMouseState.MiddleButton == ButtonState.Pressed)
            {
                return true;
            }

            previousMouseState = currentState;

            return false;
        }

        public Point GetMousePosition()
        {
            var scale = sceneManager.Scene.DestinationRectangle.Size / sceneManager.Scene.Size;
            var offset = sceneManager.Scene.DestinationRectangle.Location;

            return (Mouse.GetState().Position - offset) / scale;
        }
    }
}