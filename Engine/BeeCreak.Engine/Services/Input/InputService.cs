using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Engine.Services
{
    public interface IMouseInputService
    {
        bool DidLeftClick();

        bool DidRightClick();

        Point GetMousePosition();    
    }

    public class MouseInputService(App app) : IMouseInputService
    {
        private MouseState previous = Mouse.GetState();

        private MouseState current = Mouse.GetState();

        public void Update()
        {
            previous = current;
            current = Mouse.GetState();
        }

        public bool DidLeftClick()
        {
            return current.LeftButton == ButtonState.Released
                && previous.LeftButton == ButtonState.Pressed;
        }

        public bool DidRightClick()
        {
            return current.RightButton == ButtonState.Released
                && previous.RightButton == ButtonState.Pressed;
        }

        public Point GetMousePosition()
        {
            return app.ScreenService.ToScreenCoordinates(current.Position);
        }
    }

    public class KeyboardInputService(App app)
    {
        private KeyboardState previous = Keyboard.GetState();

        private KeyboardState current = Keyboard.GetState();

        public void Update()
        {
            previous = current;
            current = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key)
        {
            return current.IsKeyDown(key);
        }

        public bool DidKeyCycle(Keys key)
        {
            return current.IsKeyUp(key) && previous.IsKeyDown(key);
        }
    }
}