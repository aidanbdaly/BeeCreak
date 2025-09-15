using BeeCreak.Shared.Services.Dynamic;
using BeeCreak.src.Models;
using Microsoft.Xna.Framework;
using BeeCreak.Engine.Core;

namespace BeeCreak
{

    public class PlayerBehavior : IBehavior
    {
        private readonly Player player;

        private readonly EntityManager entityManager;

        private readonly TileManager tileManager;

        public PlayerBehavior(Player player, EntityManager entityManager, TileManager tileManager)
        {
            this.player = player;
            this.entityManager = entityManager;
            this.tileManager = tileManager;

            entityManager.StateImported += (sender, args) =>
            {
                PlayerEntity = entityManager.PlayerEntity;
            };
        }

        private Entity PlayerEntity { get; set; }

        public void Update(GameTime gameTime)
        {
            if (PlayerEntity == null) return;

            var multiplier = PlayerEntity.Attributes.Asset.BaseVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

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
            if (CanMoveTo(PlayerEntity.State.Position + delta))
            {
                PlayerEntity.State.Position += delta;
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
