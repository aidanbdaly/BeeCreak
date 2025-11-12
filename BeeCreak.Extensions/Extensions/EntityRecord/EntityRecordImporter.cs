using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.EntityRecord;

[ContentImporter(".erec", DisplayName = "Entity Record Importer", DefaultProcessor = "EntityRecordProcessor")]
public sealed class EntityRecordImporter : JsonImporter<EntityRecordDto>
{
}
