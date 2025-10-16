using BeeCreak.Content.Pipeline.Extensions;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions;

[ContentImporter(".json", DisplayName = "Entity Attributes Importer", DefaultProcessor = "EntityAttributesProcessor")]
public class EntityAttributesImporter : JsonImporter<EntityAttributesDTO>
{
}
