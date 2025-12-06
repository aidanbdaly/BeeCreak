using BeeCreak.Engine.Behaviours;
using BeeCreak.Engine.Data.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Engine.Graphics
{
    public class AnimationComponent(App app, Animation animation) : DrawableGameComponent(app)
    {
        public ImperativeTexture Sprite { get; } = new(app, animation.SpriteSheet.Texture);

        public Ticker Ticker { get; } = new(app, 1);

        public override void Initialize()
        {
            Ticker.OnTick += (tickCount) =>
            {
                var spriteName = animation.Data[tickCount % animation.Data.Count];
                var frame = animation.SpriteSheet.Frames[spriteName];
                Sprite.SourceRectangle.Set(frame);
            };
        }

        public override void Update(GameTime gameTime)
        {
            Ticker.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(gameTime);
        }
    }
}