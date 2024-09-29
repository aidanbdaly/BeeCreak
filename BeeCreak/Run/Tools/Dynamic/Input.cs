using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.Tools;

public class Input : IDynamic
{
    public KeyboardState PreviousState;

    public Input()
    {
        PreviousState = Keyboard.GetState();
    }

    public bool OnKeyClick(Keys key)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyUp(key) && PreviousState.IsKeyDown(key))
        {
            Console.WriteLine("Key clicked");
            Console.WriteLine(key);
        }

        return keyboardState.IsKeyUp(key) && PreviousState.IsKeyDown(key);
    }

    public void Update(GameTime gameTime)
    {
        PreviousState = Keyboard.GetState();
    }
}
