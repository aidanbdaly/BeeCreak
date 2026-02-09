using BeeCreak.Engine;
using BeeCreak.Engine.Services;
using BeeCreak.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Game.Domain.Entity
{
    public class EntityControlComponent(App app, EntityReference entity) : GameComponent(app)
    {
        private CollisionService CollisionService
            => app.Services.GetService<CollisionService>();

        private KeyboardInputService Keyboard
            => app.Services.GetService<KeyboardInputService>();

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.IsKeyDown(Keys.W))
            {
                MoveEntity(-Vector2.UnitY);
            }

            if (Keyboard.IsKeyDown(Keys.S))
            {
                MoveEntity(Vector2.UnitY);
            }

            if (Keyboard.IsKeyDown(Keys.A))
            {
                MoveEntity(-Vector2.UnitX);
            }

            if (Keyboard.IsKeyDown(Keys.D))
            {
                MoveEntity(Vector2.UnitX);
            }
        }

        private void MoveEntity(Vector2 delta)
        {
            var animation = delta switch
            {
                var d when d.Y < 0 => "walkUp",
                var d when d.Y > 0 => "walkDown",
                var d when d.X < 0 => "walkLeft",
                var d when d.X > 0 => "walkRight",
                _ => "walkRight"
            };

            entity.State.Animation.Set(entity.Base.AnimationCollection.Data[animation]);
            entity.State.Position.Set((prev) => prev + delta);
        }
    }
}
