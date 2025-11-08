using BeeCreak.Content.Pipeline.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.CellReference;

[ContentImporter(".cref", DisplayName = "Cell Reference Importer", DefaultProcessor = "CellReferenceProcessor")]
public sealed class CellReferenceImporter : JsonImporter<CellReferenceDto>
{
}
