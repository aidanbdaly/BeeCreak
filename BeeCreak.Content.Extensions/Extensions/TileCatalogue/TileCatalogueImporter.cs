using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentImporter(".json", DisplayName = "Tile Importer", DefaultProcessor = "TileCatalogueProcessor")]

public sealed class TileCatalogueImporter : JsonImporter<TileCatalogueDto> {}