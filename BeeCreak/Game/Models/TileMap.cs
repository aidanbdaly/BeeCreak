using BeeCreak.Core.Models;
using BeeCreak.Core.State;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Models;

public class TileState(string spriteName)
{
    public State<string> SpriteName { get; init; } = new(spriteName);
}

public record TileData(Point Position, TileState State);

public class TileMap(
    string id,
    SpriteSheet spriteSheet,
    BoundingBoxSheet boundingBoxSheet,
    Dictionary<Point, string> data
)
{
    public string Id { get; init; } = id;

    public SpriteSheet SpriteSheet { get; init; } = spriteSheet;

    public BoundingBoxSheet BoundingBoxSheet { get; init; } = boundingBoxSheet;

    private readonly Dictionary<Point, TileState> data = data.ToDictionary(
        kvp => kvp.Key,
        kvp => new TileState(kvp.Value)
    );

    public TileState? this[Point position] => data.TryGetValue(position, out TileState? value) ? value : null;

    public IEnumerable<TileData> Enumerate()
    {
        foreach (var entry in data)
        {
            yield return new TileData(entry.Key, entry.Value);
        }
    }
}
