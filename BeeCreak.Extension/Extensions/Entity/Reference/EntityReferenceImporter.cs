using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.Entity.Reference;

[ContentImporter(EntityReferenceConfig.FileExtension, DisplayName = EntityReferenceConfig.ImporterDisplayName, DefaultProcessor = EntityReferenceConfig.DefaultProcessor)]
public sealed class EntityReferenceImporter : JsonImporter<EntityReferenceDTO>
{
}
