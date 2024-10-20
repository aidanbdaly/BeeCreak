using BeeCreak.Tools;

namespace BeeCreak.Game.Scene.Entity.Instances.Character;

public class CharacterDTO : EntityDTO
{
    public override Character FromDTO(IToolCollection tools)
    {
        return new Character(tools, WorldPosition)
        {
            EntityType = EntityType,
            Direction = Direction,
            Speed = Speed
        };
    }
}
