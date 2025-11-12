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
            if (CanMoveTo(entity.State.Position + delta))
            {
                entity.State.Position += delta;
            }
        }

        private bool CanMoveTo(Vector2 newPosition)
        {
            entity.Base.BoundingBoxSheet.BoundingBoxes.TryGetValue(
                entity.State.Direction.ToString(),
                out var entityBoundingBoxImmutableRectangle
            );
            
            var entityBoundingBox = entityBoundingBoxImmutableRectangle.ToRectangle();

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
                Rectangle boundingBox = Rectangle.Empty;

                var tileKey = new Point(point.X + offset.X, point.Y + offset.Y);
                if (tileMap.Tiles.TryGetValue(tileKey, out var tileId) &&
                    tileMap.BoundingBoxSheet.BoundingBoxes.TryGetValue(tileId, out var box))
                {
                    boundingBox = box.ToRectangle();
                }

                yield return boundingBox;
            }
        }
    }
}
