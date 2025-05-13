using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentImporter(".spritesheet", DisplayName = "SpriteSheet Importer", DefaultProcessor = "SpriteSheetProcessor")]
public sealed class SpriteSheetImporter : JsonImporter<SpriteSheetDTO> { }
