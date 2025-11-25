using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.BoundingBoxSheet;

[ContentImporter(".bbs", DisplayName = "Bounding Box Sheet Importer", DefaultProcessor = "BoundingBoxSheetProcessor")]
public sealed class BoundingBoxSheetImporter : JsonImporter<BoundingBoxSheetDto>
{
}
