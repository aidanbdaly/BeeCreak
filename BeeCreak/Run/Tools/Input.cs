using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.Tools;

public class Input : IDynamic
{
    public KeyboardState PreviousState;
    private readonly Game Game;

    public Input(Game game)
    {
        Game = game;
    }

    public bool OnKeyClick(Keys key)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        return keyboardState.IsKeyUp(key) && PreviousState.IsKeyDown(key);
    }

    public void Update(GameTime gameTime)
    {
        if (OnKeyClick(Keys.Escape))
        {
            Game.Exit();
        }

        PreviousState = Keyboard.GetState();
    }
}
