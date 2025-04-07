using BeeCreak.Shared;
using BeeCreak.Shared.Services.Dynamic;
using BeeCreak.Shared.Services.Static;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class ControlBehavior : IDynamic
{
    private readonly IInput input;

    public ControlBehavior(IInput input)
    {
        this.input = input;
    }

    private Entity Entity { get; set; }

    public void SetEntity(Entity entity)
    {
        Entity = entity;
    }

    public void Update(GameTime gameTime)
    {
        var isMoving = false;
        var speed = Entity.Speed;
        var direction = Direction.North;

        if (input.OnActionHold(InputAction.Up))
        {
            isMoving = true;
            direction = Direction.North;
        }

        if (input.OnActionHold(InputAction.Down))
        {
            isMoving = true;
            direction = Direction.South;
        }

        if (input.OnActionHold(InputAction.Left))
        {
            isMoving = true;
            direction = Direction.West;
        }

        if (input.OnActionHold(InputAction.Right))
        {
            isMoving = true;
            direction = Direction.East;
        }

        if (input.OnActionHold(InputAction.Up) && input.OnActionHold(InputAction.Right))
        {
            isMoving = true;
            direction = Direction.NorthEast;
        }

        if (input.OnActionHold(InputAction.Up) && input.OnActionHold(InputAction.Left))
        {
            isMoving = true;
            direction = Direction.NorthWest;
        }

        if (input.OnActionHold(InputAction.Down) && input.OnActionHold(InputAction.Right))
        {
            isMoving = true;
            direction = Direction.SouthEast;
        }

        if (input.OnActionHold(InputAction.Down) && input.OnActionHold(InputAction.Left))
        {
            isMoving = true;
            direction = Direction.SouthWest;
        }

        if (isMoving)
        {
            Entity.Move(Vector2.Normalize(direction.Value) * ((float)(speed * gameTime.ElapsedGameTime.TotalSeconds)));
        }
    }
}