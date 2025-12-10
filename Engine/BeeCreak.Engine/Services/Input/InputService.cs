using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Engine.Services
{
    public class InputService(App app)
    {
        private KeyboardState previous = Keyboard.GetState();

        private KeyboardState current = Keyboard.GetState();

        public bool IsKeyDown(Keys key)
        {
            return current.IsKeyDown(key);
        }

        public bool DidKeyCycle(Keys key)
        {
            return current.IsKeyUp(key) && previous.IsKeyDown(key);
        }

        public void Update()
        {
            previous = current;
            current = Keyboard.GetState();
        }

        public Point GetMousePosition()
        {
            return app.VirtualScreenService.ToVirtualScreenCoordinates(Mouse.GetState().Position);
        }
    }
}