using BeeCreak.Content.Pipeline.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.EntityReference;

[ContentImporter(".eref", DisplayName = "Entity Reference Importer", DefaultProcessor = "EntityReferenceProcessor")]
public sealed class EntityReferenceImporter : JsonImporter<EntityReferenceDto>
{
}
