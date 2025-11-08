using BeeCreak.Content.Pipeline.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.TileMap;

[ContentImporter(".tref", DisplayName = "Tile Map Importer", DefaultProcessor = "TileMapProcessor")]
public sealed class TileMapImporter : JsonImporter<TileMapDto>
{
}
