namespace BeeCreak.Game.Scene.Entity
{
    using System;
    using global::BeeCreak.Game.Scene.Entity.Instances.Character;
    using global::BeeCreak.Game.Scene.Entity.Instances.Creature;
    using global::BeeCreak.Tools.Dynamic.Input;
    using global::BeeCreak.Tools.Static;

    public class EntityFactory
    {
        private readonly IInput input;

        private readonly IEventManager events;

        public EntityFactory(IInput input, IEventManager events)
        {
            this.input = input;
            this.events = events;
        }

        public static EntityDTO CreateEntityDTO(IEntity entity)
        {
            return entity.Type.Id switch
            {
                0 => new CharacterDTO
                {
                    Type = EntityType.Character,
                    WorldPosition = entity.WorldPosition,
                    Direction = entity.Direction,
                    Speed = entity.Speed,
                },
                1 => new CreatureDTO
                {
                    Type = EntityType.Creature,
                    WorldPosition = entity.WorldPosition,
                    Direction = entity.Direction,
                    Speed = entity.Speed,
                },
                _ => throw new ArgumentException("An error occurred while creating an entity DTO: unknown entity type."),
            };
        }

        public IEntity CreateEntity(EntityDTO entityDTO)
        {
            return entityDTO.Type.Id switch
            {
                0 => new Character(input, events)
                {
                    Type = EntityType.Character,
                    WorldPosition = entityDTO.WorldPosition,
                    Direction = entityDTO.Direction,
                    Speed = entityDTO.Speed,
                },
                1 => new Creature(input)
                {
                    Type = EntityType.Creature,
                    WorldPosition = entityDTO.WorldPosition,
                    Direction = entityDTO.Direction,
                    Speed = entityDTO.Speed,
                },
                _ => throw new ArgumentException("An error occurred while creating an entity: unknown entity type."),
            };
        }
    }
}