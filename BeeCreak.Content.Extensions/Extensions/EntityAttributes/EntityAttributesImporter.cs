using BeeCreak.Content.Extensions;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentImporter(".json", DisplayName = "Entity Attributes Importer", DefaultProcessor = "EntityAttributesProcessor")]
public class EntityAttributesImporter : JsonImporter<EntityAttributesDTO>
{
}
