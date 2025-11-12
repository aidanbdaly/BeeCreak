using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.CellReference;

[ContentImporter(".cref", DisplayName = "Cell Reference Importer", DefaultProcessor = "CellReferenceProcessor")]
public sealed class CellReferenceImporter : JsonImporter<CellReferenceDto>
{
}
