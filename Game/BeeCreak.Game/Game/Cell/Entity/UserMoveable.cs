using BeeCreak.Engine;
using BeeCreak.Engine.Services;
using BeeCreak.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Game.Domain.Entity
{
    public class UserMoveable(App app, EntityReference entity) : IGameComponent
    {
        private CollisionService CollisionService
            => app.Services.GetService<CollisionService>();

        private DirectionalInputComponent DirectionalInputService
             => app.Services.GetService<DirectionalInputComponent>();

        public void Initialize()
        {
            DirectionalInputService.OnInput += (sender, buttonMap) =>
            {
                Console.WriteLine(buttonMap);

                var delta = buttonMap switch
                {
                    { Button: Buttons.DPadUp, } => new Vector2(0, -1),
                    { Button: Buttons.DPadDown, } => new Vector2(0, 1),
                    { Button: Buttons.DPadLeft, } => new Vector2(-1, 0),
                    { Button: Buttons.DPadRight, } => new Vector2(1, 0),
                    _ => Vector2.Zero
                };



                entity.State.Position.Set((prev) => prev + delta);
            };
        }
    }

    public class CollisionService
    {
        private readonly List<Polygon> collidables = [];

        public bool CanMoveBy(Polygon a, Vector2 delta)
        {
            return collidables.All(b => !a.With(position => position + delta).Intersects(b));
        }
    }

    public struct Polygon(params Vector2[] vertices)
    {
        public Vector2 Position { get; init; } = Vector2.Zero;

        public Vector2[] Vertices { get; init; } = vertices;

        public bool Intersects(Polygon other)
        {
            return false;
        }

        public Polygon With(Func<Vector2, Vector2> setStateDelegate)
        {
            return new Polygon(Vertices) { Position = setStateDelegate(Position) };
        }
    }
}
