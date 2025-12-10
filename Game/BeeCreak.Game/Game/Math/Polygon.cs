using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Math
{
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