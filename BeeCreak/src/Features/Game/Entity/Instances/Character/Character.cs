namespace BeeCreak.Game.Scene.Entity.Instances.Character
{
    using global::BeeCreak.Game.Camera;
    using global::BeeCreak.Tools;
    using global::BeeCreak.Tools.Dynamic.Input;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;

    public class Character : ControllableEntity
    {

        public Character(IInput input, IEventManager events)
        : base(input)
        {
            Type = EntityType.Character;

            Direction = Direction.East;
            Variant = EntityVariant.FacingRight;

            events.Dispatch(new FocusOnEvent(this));

            Speed = 100;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
        }
    }
}
