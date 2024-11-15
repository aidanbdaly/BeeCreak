namespace BeeCreak.Game.Scene.Entity
{
    using System;
    using global::BeeCreak.Game.Scene.Entity;
    using global::BeeCreak.Game.Scene.Entity.Instances.Character;
    using global::BeeCreak.Game.Scene.Entity.Instances.Creature;
    using global::BeeCreak.Tools.Dynamic.Input;
    using global::BeeCreak.Tools.Static;

    public class EntityFactory
    {
        private readonly ISprite sprite;

        private readonly IInput input;

        private readonly IEventManager events;

        public EntityFactory(ISprite sprite, IInput input, IEventManager events)
        {
            this.sprite = sprite;
            this.input = input;
            this.events = events;
        }

        public static EntityDTO CreateEntityDTO(Entity entity)
        {
            return entity.Type switch
            {
                EntityType.Character => new CharacterDTO
                {
                    Type = EntityType.Character,
                    WorldPosition = entity.WorldPosition,
                    Direction = entity.Direction,
                    Speed = entity.Speed,
                },
                EntityType.Creature => new CreatureDTO
                {
                    Type = EntityType.Creature,
                    WorldPosition = entity.WorldPosition,
                    Direction = entity.Direction,
                    Speed = entity.Speed,
                },
                _ => throw new ArgumentException("An error occurred while creating an entity DTO: unknown entity type."),
            };
        }

        public Entity CreateEntity(EntityDTO entityDTO)
        {
            return entityDTO.Type switch
            {
                EntityType.Character => new Character(sprite, input, events)
                {
                    Type = EntityType.Character,
                    WorldPosition = entityDTO.WorldPosition,
                    Direction = entityDTO.Direction,
                    Speed = entityDTO.Speed,
                },
                EntityType.Creature => new Creature(sprite, input)
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