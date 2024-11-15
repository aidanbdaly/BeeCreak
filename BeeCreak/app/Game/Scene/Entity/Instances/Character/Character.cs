namespace BeeCreak.Game.Scene.Entity.Instances.Character
{
    using System.Collections.Generic;
    using global::BeeCreak.Game.Camera;
    using global::BeeCreak.Tools;
    using global::BeeCreak.Tools.Dynamic.Input;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Character : ControllableEntity
    {
        private readonly ISprite sprite;

        public Character(ISprite sprite, IInput input, IEventManager events)
        : base(input)
        {
            this.sprite = sprite;

            TextureVariants = new Dictionary<Direction, Texture2D>
            {
                { Direction.North, sprite.GetTexture("man-up") },
                { Direction.South, sprite.GetTexture("man-down") },
                { Direction.West, sprite.GetTexture("man-left") },
                { Direction.East, sprite.GetTexture("man-right") },
            };

            Type = EntityType.Character;

            Direction = Direction.East;
            ActiveTexture = TextureVariants[Direction];

            events.Dispatch(new FocusOnEvent(this));

            Speed = 100;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
        }

        public override void Draw()
        {
            sprite.Batch.Draw(ActiveTexture, WorldPosition, Color.White);
        }
    }
}
