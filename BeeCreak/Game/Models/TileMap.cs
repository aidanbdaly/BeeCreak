using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game.Models;

public record TileData(Point Position, string SpriteName);

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

    private readonly Dictionary<Point, string> data = data;


    public string? this[Point position] => data.TryGetValue(position, out var value) ? value : null;

    public IEnumerable<TileData> Enumerate()
    {
        foreach (var entry in data)
        {
            yield return new TileData(entry.Key, entry.Value);
        }
    }
}
