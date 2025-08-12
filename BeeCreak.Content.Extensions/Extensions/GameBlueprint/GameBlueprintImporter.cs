using BeeCreak.Content.Extensions;
using Microsoft.Xna.Framework.Content.Pipeline;

[ContentImporter(".json", DisplayName = "Game Blueprint Importer", DefaultProcessor = "GameBlueprintProcessor")]
public class GameBlueprintImporter : JsonImporter<GameBlueprintDTO> { }
