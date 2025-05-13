using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.Content.Extensions;

[ContentImporter(".animation", DisplayName = "Animation Importer", DefaultProcessor = "AnimationProcessor")]
public sealed class AnimationImporter : JsonImporter<AnimationDTO> {}