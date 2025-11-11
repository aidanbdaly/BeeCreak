using BeeCreak.App.Game.Domain.Tile;
using BeeCreak.App.Game.Models;
using BeeCreak.Core.Components;
using BeeCreak.Core.Input;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Domain.Entity
{
    public class ControlBehaviour(InputManager input, TileMapRecord tileMap, EntityReference entity) : Updateable
    {
        private readonly InputManager input = input;

        private readonly TileMapRecord tileMap = tileMap;

        private readonly EntityReference entity = entity;

        public override void Update(GameTime gameTime)
        {
            var multiplier = 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (input.ButtonCycled(ButtonMap.Up))
            {
                Move(-Vector2.UnitY * multiplier);
            }

            if (input.ButtonCycled(ButtonMap.Down))
            {
                Move(Vector2.UnitY * multiplier);
            }

            if (input.ButtonCycled(ButtonMap.Left))
            {
                Move(-Vector2.UnitX * multiplier);
            }

            if (input.ButtonCycled(ButtonMap.Right))
            {
                Move(Vector2.UnitX * multiplier);
            }
        }

        private void Move(Vector2 delta)
        {
            if (CanMoveTo(entity.Position + delta))
            {
                entity.Position += delta;
            }
        }

        private bool CanMoveTo(Vector2 newPosition)
        {
            entity.Base.BoundingBoxSheet.BoundingBoxes.TryGetValue(
                entity.Variant,
                out var entityBoundingBox
            );

            foreach (Rectangle tileBoundingBox in TileAndNeighbourBoundingBoxes((newPosition / 32).ToPoint()))
            {
                if (entityBoundingBox.Intersects(tileBoundingBox))
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<Rectangle> TileAndNeighbourBoundingBoxes(Point point)
        {
            foreach (var offset in TileUtils.PointWithNeighbors)
            {
                Rectangle boundingBox;

                try
                {
                    tileMap.BoundingBoxSheet.BoundingBoxes.TryGetValue(
                        tileMap.Tiles[new Point(point.X + offset.X, point.Y + offset.Y)],
                        out var box);

                    boundingBox = box;
                }
                catch (ArgumentNullException)
                {
                    boundingBox = Rectangle.Empty;
                }

                yield return boundingBox;
            }
        }
    }
}
