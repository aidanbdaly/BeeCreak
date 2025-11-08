using BeeCreak.App.Game.Domain.Tile;
using BeeCreak.App.Game.Models;
using BeeCreak.Core.Components;
using BeeCreak.Core;
using BeeCreak.Core.Input;
using Microsoft.Xna.Framework;

namespace BeeCreak.App.Game.Domain.Entity
{
    public class Controllable(Input input) : Updateable
    {
        private readonly Input input = input;

        private TileMap tileMap;

        private EntityReference reference;

        public void Bind(EntityReference reference, TileMap tileMap)
        {
            this.reference = reference;
            this.tileMap = tileMap;
        }

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
            if (CanMoveTo(reference.Position + delta))
            {
                reference.Position += delta;
            }
        }

        private bool CanMoveTo(Vector2 newPosition)
        {
            foreach (TileRecord tile in tileMap.WithNeighbours((newPosition / 32).ToPoint()))
            {
                //if (tile.HasCollision && tile.Attributes.HitBox.Intersects(PlayerEntity.AbsoluteHitBox))
                //{
                //    return false;
                //}
            }

            return true;
        }
    }
}
