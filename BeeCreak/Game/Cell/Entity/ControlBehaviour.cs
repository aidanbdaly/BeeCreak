using BeeCreak.Core.Input;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Domain.Entity
{
    public class Directional(InputManager input)
    {
        public event EventHandler<Vector2>? OnDirectionChanged;

        public void Update(GameTime gameTime)
        {
            var vector = Vector2.Zero;

            if (input.ButtonCycled(ButtonMap.Up))
            {
                vector += -Vector2.UnitY;
            }

            if (input.ButtonCycled(ButtonMap.Down))
            {
                vector += Vector2.UnitY;
            }

            if (input.ButtonCycled(ButtonMap.Left))
            {
                vector += -Vector2.UnitX;
            }

            if (input.ButtonCycled(ButtonMap.Right))
            {
                vector += Vector2.UnitX;
            }

            if (vector != Vector2.Zero)
            {
                OnDirectionChanged?.Invoke(this, vector);
            }
        }
    }

    public class CollisionService(List<Polygon> collidables)
    {
        public bool CanMoveBy(Polygon a, Vector2 delta)
        {
            return collidables.All(other => !a.At(position => position + delta).Intersects(other));
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

        public Polygon At(Func<Vector2, Vector2> setStateDelegate)
        {
            return new Polygon(Vertices) { Position = setStateDelegate(Position) };
        }
    }
}
