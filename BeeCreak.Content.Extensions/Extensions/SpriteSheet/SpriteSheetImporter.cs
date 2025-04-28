using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.ContentPipeline;

[ContentImporter(".spritesheet", DisplayName = "Spritesheet Importer", DefaultProcessor = "SpritesheetProcessor")]
internal sealed class SpriteSheetImporter : JsonImporter<SpriteSheetDTO> { }
