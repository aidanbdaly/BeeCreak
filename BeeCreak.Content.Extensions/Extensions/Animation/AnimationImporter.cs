using BeeCreak.Shared.Data.Models;

using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.ContentPipeline;

[ContentImporter(".animation", DisplayName = "Animation Importer", DefaultProcessor = "AnimationProcessor")]
internal sealed class AnimationImporter : JsonImporter<AnimationDTO> {}