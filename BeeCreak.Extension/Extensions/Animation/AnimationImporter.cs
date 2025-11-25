using BeeCreak.Extensions.Core;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Extensions.Animation;

[ContentImporter(".as", DisplayName = "Animation Importer", DefaultProcessor = "AnimationProcessor")]
public sealed class AnimationSheetImporter : JsonImporter<AnimationDto>
{
}
