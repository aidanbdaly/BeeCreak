namespace BeeCreak.Game.Scene.Entity.Instances.Creature
{
    using global::BeeCreak.Tools.Dynamic.Input;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;

    public class Creature : Entity
    {
        private readonly ISprite sprite;

        private readonly IInput input;

        public Creature(ISprite sprite, IInput input)
        {
            this.sprite = sprite;
            this.input = input;

            Type = EntityType.Creature;

            Speed = 50;

            ActiveTexture = sprite.GetTexture("creature");
        }

        public bool IsMoving { get; set; } = false;

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

        public override void Draw()
        {
            sprite.Batch.Draw(ActiveTexture, WorldPosition, Color.White);
        }
    }
}