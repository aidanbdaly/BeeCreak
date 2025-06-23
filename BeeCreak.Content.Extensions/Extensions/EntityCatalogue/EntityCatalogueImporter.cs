using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentImporter(".json", DisplayName = "Entity Importer", DefaultProcessor = "EntityProcessor")]

public sealed class EntityCatalogueImporter : JsonImporter<EntityCatalogueDTO> {}