using BeeCreak.Scene.Main;
using BeeCreak.Shared.Data.Config;
using BeeCreak.Shared.Services;
using BeeCreak.Shared.Services.Dynamic;
using Microsoft.Xna.Framework;

public class PlayerBehavior(EntityManager entityManager, TileManager tileManager, Player player) : IBehavior
{
    private readonly Player player = player;

    private readonly EntityManager entityManager = entityManager;

    private readonly TileManager tileManager = tileManager;

    private readonly Point[] AdjacentOffsets =
    [
        new(-1, -1), new(0, -1), new(1, -1),
        new(-1, 0),               new(1, 0),
        new(-1, 1),  new(0, 1),  new(1, 1)
    ];

    public void Update(GameTime gameTime)
    {
        var playerVelocity = entityManager.Velocities[entityManager.Player];

        if (player.OnAction(PlayerAction.Up))
        {
            TryMove(new Vector2(0, -1) * (float)playerVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        if (player.OnAction(PlayerAction.Down))
        {
            var amount = new Vector2(0, 1) * (float)playerVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            TryMove(amount);
        }

        if (player.OnAction(PlayerAction.Left))
        {
            var amount = new Vector2(-1, 0) * (float)playerVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            TryMove(amount);
        }

        if (player.OnAction(PlayerAction.Right))
        {
            var amount = new Vector2(1, 0) * (float)playerVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            TryMove(amount);
        }
    }

    private void TryMove(Vector2 delta)
    {
        var playerId = entityManager.Player;

        var playerPosition = entityManager.Positions[playerId];

        var newPosition = playerPosition.Position + delta;

        var playerBox = EntityHitBoxes.GetHitBox(entityManager.GetPlayer().Sprite);

        playerBox.Offset((int)newPosition.X, (int)newPosition.Y);

        var tileCoord = WorldToTile(newPosition);

        if (CheckCollision(tileCoord, newPosition))
        {
            return;
        }

        foreach (Point offset in AdjacentOffsets)
        {
            var neighborTileCoord = new Point(tileCoord.X + offset.X, tileCoord.Y + offset.Y);

            if (CheckCollision(neighborTileCoord, newPosition))
            {
                return;
            }
        }

        playerPosition.Position = newPosition;
    }

    private bool CheckCollision(Point tileCoord, Vector2 newPosition)
    {
        if (!tileManager.InBounds(tileCoord)) return false;

        var neighborTile = tileManager.Tiles[tileCoord.X, tileCoord.Y];
        var tileBox = TileHitBoxes.GetHitBox(neighborTile.Type);

        var neighborWorld = TileToWorld(tileCoord);
        tileBox.Offset(neighborWorld.X, neighborWorld.Y);

        var playerBox = EntityHitBoxes.GetHitBox(entityManager.GetPlayer().Sprite);
        playerBox.Offset((int)newPosition.X, (int)newPosition.Y);

        return playerBox.Intersects(tileBox);
    }

    private static Point WorldToTile(Vector2 pos) =>
        new(
            (int)MathF.Floor(pos.X / EngineConfig.TILE_RESOLUTION),
            (int)MathF.Floor(pos.Y / EngineConfig.TILE_RESOLUTION));

    private static Point TileToWorld(Point tile) =>
        new(
            tile.X * EngineConfig.TILE_RESOLUTION,
            tile.Y * EngineConfig.TILE_RESOLUTION);

}