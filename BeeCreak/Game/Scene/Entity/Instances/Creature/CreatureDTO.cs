using BeeCreak.Tools;

namespace BeeCreak.Game.Scene.Entity.Instances.Creature;

public class CreatureDTO : EntityDTO
{
    public bool IsMoving { get; set; }

    public override Creature FromDTO(IToolCollection tools)
    {
        return new Creature(tools, WorldPosition)
        {
            EntityType = EntityType,
            Direction = Direction,
            Speed = Speed,
            IsMoving = IsMoving
        };
    }
}
