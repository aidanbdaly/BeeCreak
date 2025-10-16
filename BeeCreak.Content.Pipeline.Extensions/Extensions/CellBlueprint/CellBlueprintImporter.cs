using BeeCreak.Content.Pipeline.Extensions;
using Microsoft.Xna.Framework.Content.Pipeline;

[ContentImporter(".json", DisplayName = "Cell Blueprint Importer", DefaultProcessor = "CellBlueprintProcessor")]
public class CellBlueprintImporter : JsonImporter<CellBlueprintDTO> { }
