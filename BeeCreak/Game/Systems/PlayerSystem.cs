using BeeCreak.Scene.Main;
using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Services.Dynamic;
using Microsoft.Xna.Framework;

public class PlayerSystem : IDisposable
{
    private readonly Player player;

    private readonly EntityManager entityManager;

    private readonly TileManager tileManager;

    private readonly SystemManager systemManager;

    public PlayerSystem(SystemManager systemManager, EntityManager entityManager, TileManager tileManager, Player player)
    {
        this.player = player;
        this.tileManager = tileManager;
        this.entityManager = entityManager;
        this.systemManager = systemManager;

        entityManager.StateImported += ImportPlayerState;
    }

    private EntityState? PlayerState { get; set; }

    private static readonly List<Point> AdjacentTilePositions =
     (from x in Enumerable.Range(-1, 3)
      from y in Enumerable.Range(-1, 3)
      select new Point(x, y)).ToList();

    public void Initialize() {
        systemManager.AddSystem(Update);
    }

    public void Dispose()
    {
        entityManager.StateImported -= ImportPlayerState;
        
        systemManager.RemoveSystem(Update);
    }

    private void Update(GameTime gameTime)
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

    private void TryMove(Vector2 amount)
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

    private void ImportPlayerState(object? sender, EventArgs e)
    {
        PlayerState = entityManager.GetPlayer();
    }
}