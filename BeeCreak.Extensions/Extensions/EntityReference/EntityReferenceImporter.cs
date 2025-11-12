using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.EntityReference;

[ContentImporter(".eref", DisplayName = "Entity Reference Importer", DefaultProcessor = "EntityReferenceProcessor")]
public sealed class EntityReferenceImporter : JsonImporter<EntityReferenceDto>
{
}
