using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.CellRecord;

[ContentImporter(".cell", DisplayName = "Cell Record Importer", DefaultProcessor = "CellRecordProcessor")]
public sealed class CellRecordImporter : JsonImporter<CellRecordDto>
{
}
