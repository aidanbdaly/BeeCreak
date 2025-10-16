using BeeCreak.Engine.Assets;
using BeeCreak.Engine.Components;
using BeeCreak.Shared.Services.Dynamic;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak
{
    public class PlayerEntity : SpriteComponent
    {
        private readonly EntityState state;

        private readonly EntityAttributes attributes;

        public PlayerEntity(EntityProps props) : base(spriteSheet, state.Variant)
        {
            this.state = state;
        }

        public override void Update(GameTime gameTime)
        {
            var multiplier = attributes.Asset.BaseVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (player.OnAction(PlayerAction.Up))
            {
                Move(-Vector2.UnitY * multiplier);
            }

            if (player.OnAction(PlayerAction.Down))
            {
                Move(Vector2.UnitY * multiplier);
            }

            if (player.OnAction(PlayerAction.Left))
            {
                Move(-Vector2.UnitX * multiplier);
            }

            if (player.OnAction(PlayerAction.Right))
            {
                Move(Vector2.UnitX * multiplier);
            }
        }

        private void Move(Vector2 delta)
        {
            if (CanMoveTo(state.Position + delta))
            {
                state.Position += delta;
            }
        }

        private bool CanMoveTo(Vector2 newPosition)
        {
            //foreach (Tile tile in tileManager.WithNeighbors((newPosition / GameConfig.TILE_SIZE).ToPoint()))
            // {
            //    if (tile.HasCollision && tile.Attributes.HitBox.Intersects(PlayerEntity.AbsoluteHitBox))
            //    {
            //          return false;
            //    }
            //  }

            return true;
        }
    }
}
