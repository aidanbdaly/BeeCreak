using BeeCreak.Shared.Services.Dynamic;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class ControlBehavior : IControlBehavior
{
    private readonly IInput input;

    public ControlBehavior(IInput input)
    {
        this.input = input;
    }

    private IEntity Entity { get; set; }

    public void SetEntity(IEntity entity)
    {
        Entity = entity;
    }

    public void Update(GameTime gameTime)
    {
        var isMoving = false;
        var speed = Entity.Speed;
        var direction = Direction.North;
        var variant = Entity.Variant;

        if (input.OnActionHold(InputAction.Up))
        {
            isMoving = true;
            direction = Direction.North;
            variant = EntityVariant.FacingNorth;
        }

        if (input.OnActionHold(InputAction.Down))
        {
            isMoving = true;
            direction = Direction.South;
            variant = EntityVariant.FacingSouth;
        }

        if (input.OnActionHold(InputAction.Left))
        {
            isMoving = true;
            direction = Direction.West;
            variant = EntityVariant.FacingWest;
        }

        if (input.OnActionHold(InputAction.Right))
        {
            isMoving = true;
            direction = Direction.East;
            variant = EntityVariant.FacingEast;
        }

        if (input.OnActionHold(InputAction.Up) && input.OnActionHold(InputAction.Right))
        {
            isMoving = true;
            direction = Direction.NorthEast;
            variant = EntityVariant.FacingEast;
        }

        if (input.OnActionHold(InputAction.Up) && input.OnActionHold(InputAction.Left))
        {
            isMoving = true;
            direction = Direction.NorthWest;
            variant = EntityVariant.FacingWest;
        }

        if (input.OnActionHold(InputAction.Down) && input.OnActionHold(InputAction.Right))
        {
            isMoving = true;
            direction = Direction.SouthEast;
            variant = EntityVariant.FacingEast;
        }

        if (input.OnActionHold(InputAction.Down) && input.OnActionHold(InputAction.Left))
        {
            isMoving = true;
            direction = Direction.SouthWest;
            variant = EntityVariant.FacingWest;
        }

        if (isMoving)
        {
            Entity.Move(Vector2.Normalize(direction.Value) * ((float)(speed * gameTime.ElapsedGameTime.TotalSeconds)));
            Entity.SetVariant(variant);
        }
    }
}