using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Domain.Entity
{
    public class EntityState
    {
        private Direction direction = Direction.Down;

        public event Action<Direction>? DirectionChanged;

        public Direction Direction
        {
            get => direction;
            set
            {
                if (direction == value)
                {
                    return;
                }

                direction = value;
                DirectionChanged?.Invoke(direction);
            }
        }

        public Vector2 Position { get; set; } = Vector2.Zero;
    }
}
