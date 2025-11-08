using BeeCreak.Content.Pipeline.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Pipeline.Extensions.AnimationSheet;

[ContentImporter(".as", DisplayName = "Animation Sheet Importer", DefaultProcessor = "AnimationSheetProcessor")]
public sealed class AnimationSheetImporter : JsonImporter<AnimationSheetDto>
{
}
