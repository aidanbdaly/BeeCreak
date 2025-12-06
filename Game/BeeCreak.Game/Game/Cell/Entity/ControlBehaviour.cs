using BeeCreak.Engine;
using BeeCreak.Engine.Input;
using BeeCreak.Engine.Services;
using BeeCreak.Game.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeeCreak.Game.Domain.Entity
{
    public interface IInputSource
    {
        event EventHandler<ButtonMap>? OnInput;
    }

    public interface IDirectionalInputService : IInputSource;

    public class DirectionalInputSource : GameComponent, IDirectionalInputService
    {
        private readonly InputService input;

        public DirectionalInputSource(App app) : base(app)
        {
            input = app.Services.GetService<InputService>();

            app.Services.AddService<IDirectionalInputService>(this);
        }

        public event EventHandler<ButtonMap>? OnInput;

        public override void Update(GameTime gameTime)
        {
            ButtonMap? direction = null;

            if (input.DidButtonCycle(ButtonMap.Up))
            {
                direction = ButtonMap.Up;
            }

            if (input.DidButtonCycle(ButtonMap.Down))
            {
                direction = ButtonMap.Down;
            }

            if (input.DidButtonCycle(ButtonMap.Left))
            {
                direction = ButtonMap.Left;
            }

            if (input.DidButtonCycle(ButtonMap.Right))
            {
                direction = ButtonMap.Right;
            }

            if (direction is not null)
            {
                OnInput?.Invoke(this, direction.Value);
            }
        }
    }

    public class UserMoveable(App app, EntityReference entity) : IGameComponent
    {
        private CollisionService CollisionService
            => app.Services.GetService<CollisionService>();

        private IDirectionalInputService DirectionalInputService
             => app.Services.GetService<IDirectionalInputService>();


        public void Initialize()
        {
            DirectionalInputService.OnInput += (sender, buttonMap) =>
            {
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
