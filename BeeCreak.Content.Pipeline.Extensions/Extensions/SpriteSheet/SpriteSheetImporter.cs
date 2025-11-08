using BeeCreak.Content.Pipeline.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.SpriteSheet;

[ContentImporter(".spritesheet", DisplayName = "SpriteSheet Importer", DefaultProcessor = "SpriteSheetProcessor")]
public sealed class SpriteSheetImporter : JsonImporter<SpriteSheetDto> { }
