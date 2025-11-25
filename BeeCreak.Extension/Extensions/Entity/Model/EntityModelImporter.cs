using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.Entity.Model;

[ContentImporter(EntityModelConfig.FileExtension, DisplayName = EntityModelConfig.ImporterDisplayName, DefaultProcessor = EntityModelConfig.DefaultProcessor)]
public sealed class EntityModelImporter : JsonImporter<EntityModelDTO>
{
}
