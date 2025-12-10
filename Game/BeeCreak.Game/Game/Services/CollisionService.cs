using BeeCreak.Game.Math;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game
{
    public class CollisionService
    {
        private readonly List<Polygon> collidables = [];

        public bool CanMoveBy(Polygon a, Vector2 delta)
        {
            return collidables.All(b => !a.With(position => position + delta).Intersects(b));
        }
    }
}