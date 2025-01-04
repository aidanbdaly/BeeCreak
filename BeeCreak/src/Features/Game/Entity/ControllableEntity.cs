namespace BeeCreak.Game.Scene.Entity
{
    using System;
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
            IsMoving = false;

            var newDirection = Direction;
            var newVariant = Variant;

            if (Input.OnActionHold(InputAction.Up))
            {
                IsMoving = true;
                newDirection = Direction.North;
                newVariant = EntityVariant.FacingUp;
            }

            if (Input.OnActionHold(InputAction.Down))
            {
                IsMoving = true;
                newDirection = Direction.South;
                newVariant = EntityVariant.FacingDown;
            }

            if (Input.OnActionHold(InputAction.Left))
            {
                IsMoving = true;
                newDirection = Direction.West;
                newVariant = EntityVariant.FacingLeft;
            }

            if (Input.OnActionHold(InputAction.Right))
            {
                IsMoving = true;
                newDirection = Direction.East;
                newVariant = EntityVariant.FacingRight;
            }

            if (Input.OnActionHold(InputAction.Up) && Input.OnActionHold(InputAction.Right))
            {
                IsMoving = true;
                newDirection = Direction.NorthEast;
                newVariant = EntityVariant.FacingRight;
            }

            if (Input.OnActionHold(InputAction.Up) && Input.OnActionHold(InputAction.Left))
            {
                IsMoving = true;
                newDirection = Direction.NorthWest;
                newVariant = EntityVariant.FacingLeft;
            }

            if (Input.OnActionHold(InputAction.Down) && Input.OnActionHold(InputAction.Right))
            {
                IsMoving = true;
                newDirection = Direction.SouthEast;
                newVariant = EntityVariant.FacingRight;
            }

            if (Input.OnActionHold(InputAction.Down) && Input.OnActionHold(InputAction.Left))
            {
                IsMoving = true;
                newDirection = Direction.SouthWest;
                newVariant = EntityVariant.FacingLeft;
            }

            if (IsMoving)
            {
                Move(newDirection, gameTime);
                Direction = newDirection;
                Variant = newVariant;
            }
        }
    }
}