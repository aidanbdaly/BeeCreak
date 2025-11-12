using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.AnimationSheet;

[ContentImporter(".as", DisplayName = "Animation Sheet Importer", DefaultProcessor = "AnimationSheetProcessor")]
public sealed class AnimationSheetImporter : JsonImporter<AnimationSheetDto>
{
}
