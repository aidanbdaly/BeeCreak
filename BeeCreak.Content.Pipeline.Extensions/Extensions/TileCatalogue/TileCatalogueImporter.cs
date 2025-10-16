using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions;

[ContentImporter(".json", DisplayName = "Tile Importer", DefaultProcessor = "TileCatalogueProcessor")]

public sealed class TileCatalogueImporter : JsonImporter<TileCatalogueDto> {}