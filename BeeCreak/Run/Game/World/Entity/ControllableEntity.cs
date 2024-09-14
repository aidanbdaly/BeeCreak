using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.GameObjects.World.Entity;

public abstract class ControllableEntity : MoveableEntity
{
    protected void HandleInput(KeyboardState keyboardState, GameTime gameTime)
    {
        var newDirection = new Direction();

        var newTexture = ActiveTexture;

        if (keyboardState.IsKeyDown(Keys.W))
        {
            newDirection = Direction.North;
            newTexture = TextureVariants[Direction.North];
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            newDirection = Direction.South;
            newTexture = TextureVariants[Direction.South];
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            newDirection = Direction.West;
            newTexture = TextureVariants[Direction.West];
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            newDirection = Direction.East;
            newTexture = TextureVariants[Direction.East];
        }
        if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.D))
        {
            newDirection = Direction.NorthEast;
            newTexture = TextureVariants[Direction.East];
        }
        if (keyboardState.IsKeyDown(Keys.W) && keyboardState.IsKeyDown(Keys.A))
        {
            newDirection = Direction.NorthWest;
            newTexture = TextureVariants[Direction.West];
        }
        if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.D))
        {
            newDirection = Direction.SouthEast;
            newTexture = TextureVariants[Direction.East];
        }
        if (keyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyDown(Keys.A))
        {
            newDirection = Direction.SouthWest;
            newTexture = TextureVariants[Direction.West];
        }

        if (newDirection.Value != Vector2.Zero)
        {
            Move(newDirection, gameTime);
            Direction = newDirection;
            ActiveTexture = newTexture;
        }
    }
}
