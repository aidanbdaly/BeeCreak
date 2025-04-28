using BeeCreak.Scene.Main;
using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Services.Dynamic;
using Microsoft.Xna.Framework;

public class PlayerSystem
{
    private readonly Player player;

    private readonly EntityManager entityManager;

    private readonly TileManager tileManager;

    public PlayerSystem(EntityManager entityManager, TileManager tileManager, Player player)
    {
        this.player = player;
        this.tileManager = tileManager;
        this.entityManager = entityManager;

        entityManager.StateImported += ImportPlayerState;
    }

    private EntityState PlayerState { get; set; }

    private static readonly List<Point> AdjacentTilePositions =
     (from x in Enumerable.Range(-1, 3)
      from y in Enumerable.Range(-1, 3)
      select new Point(x, y)).ToList();

    private void ImportPlayerState(object? sender, EventArgs e)
    {
        PlayerState = entityManager.GetPlayer();
    }

    public void Update(GameTime gameTime)
    {
        if (player.OnAction(PlayerAction.Up))
        {
            TryMove(new Vector2(0, -1));
        }

        if (player.OnAction(PlayerAction.Down))
        {
            TryMove(new Vector2(0, 1));
        }

        if (player.OnAction(PlayerAction.Left))
        {
            TryMove(new Vector2(-1, 0));
        }

        if (player.OnAction(PlayerAction.Right))
        {
            TryMove(new Vector2(1, 0));
        }
    }

    public void TryMove(Vector2 amount)
    {
        var position = PlayerState.Position + amount;

        var tile = new Point(
            (int)position.X / EngineConfig.TILE_RESOLUTION,
            (int)position.Y / EngineConfig.TILE_RESOLUTION
            );

        foreach (var point in AdjacentTilePositions)
        {
            var adjacentTile = tileManager.Tiles[tile.X + point.X, tile.Y + point.Y];

            var hitbox = new Rectangle(
                (int)position.X,
                (int)position.Y,
                PlayerState.HitBox.Width,
                PlayerState.HitBox.Height
            );

            if (hitbox.Intersects(adjacentTile.Hitbox))
            {
                return;
            }
        }

        PlayerState.Position = position;
    }
}