using BeeCreak.Content.Pipeline.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.EntityRecord;

[ContentImporter(".erec", DisplayName = "Entity Record Importer", DefaultProcessor = "EntityRecordProcessor")]
public sealed class EntityRecordImporter : JsonImporter<EntityRecordDto>
{
}
