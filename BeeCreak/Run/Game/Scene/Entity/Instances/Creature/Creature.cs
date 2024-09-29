using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Run.Game.Scene.Entity.Instances.Creature;

public class Creature : Entity
{
    public bool IsMoving { get; set; } = false;

    public Creature(IToolCollection tools, Vector2 worldPosition)
    {
        Tools = tools;
        WorldPosition = worldPosition;

        EntityType = EntityType.Creature;

        Speed = 50;

        ActiveTexture = tools.Static.Sprite.GetTexture("creature");
    }

    public override void Update(GameTime gameTime)
    {
        if (Tools.Dynamic.Input.OnKeyClick(Keys.Space))
        {
            IsMoving = !IsMoving;
        }

        if (IsMoving)
        {
            Move(Direction.East, gameTime);
        }
    }

    public override void Draw()
    {
        Tools.Static.Sprite.Batch.Draw(ActiveTexture, WorldPosition, Color.White);
    }

    public override CreatureDTO ToDTO()
    {
        return new CreatureDTO
        {
            WorldPosition = WorldPosition,
            Direction = Direction,
            EntityType = EntityType,
            Speed = Speed,
            IsMoving = IsMoving
        };
    }
}
