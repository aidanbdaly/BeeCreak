using BeeCreak.Content.Pipeline.Extensions;
using Microsoft.Xna.Framework.Content.Pipeline;

[ContentImporter(".json", DisplayName = "Cell Attributes Importer", DefaultProcessor = "CellAttributesProcessor")]
public class CellAttributesImporter : JsonImporter<CellAttributesDTO> { }