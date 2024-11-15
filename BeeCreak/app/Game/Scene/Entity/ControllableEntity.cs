namespace BeeCreak.Game.Scene.Entity
{
    using global::BeeCreak.Tools;
    using global::BeeCreak.Tools.Dynamic.Input;
    using Microsoft.Xna.Framework;

    public abstract class ControllableEntity : Entity
    {
        protected ControllableEntity(IInput input)
        {
            Input = input;
        }

        protected IInput Input { get; set; }

        protected void HandleInput(GameTime gameTime)
        {
            var newDirection = Direction;

            var newTexture = ActiveTexture;

            if (Input.OnActionHold(InputAction.Up))
            {
                newDirection = Direction.North;
                newTexture = TextureVariants[Direction.North];
            }

            if (Input.OnActionHold(InputAction.Down))
            {
                newDirection = Direction.South;
                newTexture = TextureVariants[Direction.South];
            }

            if (Input.OnActionHold(InputAction.Left))
            {
                newDirection = Direction.West;
                newTexture = TextureVariants[Direction.West];
            }

            if (Input.OnActionHold(InputAction.Right))
            {
                newDirection = Direction.East;
                newTexture = TextureVariants[Direction.East];
            }

            if (Input.OnActionHold(InputAction.Up) && Input.OnActionHold(InputAction.Right))
            {
                newDirection = Direction.NorthEast;
                newTexture = TextureVariants[Direction.East];
            }

            if (Input.OnActionHold(InputAction.Up) && Input.OnActionHold(InputAction.Left))
            {
                newDirection = Direction.NorthWest;
                newTexture = TextureVariants[Direction.West];
            }

            if (Input.OnActionHold(InputAction.Down) && Input.OnActionHold(InputAction.Right))
            {
                newDirection = Direction.SouthEast;
                newTexture = TextureVariants[Direction.East];
            }

            if (Input.OnActionHold(InputAction.Down) && Input.OnActionHold(InputAction.Left))
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
}