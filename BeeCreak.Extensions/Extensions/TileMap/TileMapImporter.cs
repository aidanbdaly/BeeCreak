using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.TileMap;

[ContentImporter(".tref", DisplayName = "Tile Map Importer", DefaultProcessor = "TileMapProcessor")]
public sealed class TileMapImporter : JsonImporter<TileMapDto>
{
}
