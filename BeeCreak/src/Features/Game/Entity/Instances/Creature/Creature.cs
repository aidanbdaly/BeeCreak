namespace BeeCreak.Game.Scene.Entity.Instances.Creature
{
    using global::BeeCreak.Tools.Dynamic.Input;
    using Microsoft.Xna.Framework;

    public class Creature : Entity
    {
        private readonly IInput input;

        public Creature(IInput input)
        {
            this.input = input;

            Type = EntityType.Creature;

            Speed = 50;
        }

        public override void Update(GameTime gameTime)
        {
            if (input.OnActionClick(InputAction.Up))
            {
                IsMoving = !IsMoving;
            }

            if (IsMoving)
            {
                Move(Direction, gameTime);
            }
        }
    }
}