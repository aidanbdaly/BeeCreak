using BeeCreak.Core;
using BeeCreak.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Core.Input
{
    public class Input(SceneManager sceneManager)
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

        public bool PointerButtonDown(PointerButtonMap button )
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

            Console.WriteLine("Checking pointer button cycled", button.Button.ToString());
            Console.WriteLine(currentState.LeftButton.ToString());

            if (
                button.Button == PointerButton.Left
                && currentState.LeftButton == ButtonState.Released
                && previousMouseState.LeftButton == ButtonState.Pressed)
            {
                Console.WriteLine("Left button cycled");
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
            var sceneWidth = sceneManager.Scene.Width;
            var sceneHeight = sceneManager.Scene.Height;

            var screen = sceneManager.DestinationRectangle;

            var mousePosition = Mouse.GetState().Position;

            float scaleX = screen.Width / (float)sceneWidth;
            float scaleY = screen.Height / (float)sceneHeight;
            int offX = screen.X;
            int offY = screen.Y;

            return new Point(
                (int)((mousePosition.X - offX) / scaleX),
                (int)((mousePosition.Y - offY) / scaleY));
        }
    }
}