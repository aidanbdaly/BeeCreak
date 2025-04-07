using BeeCreak.Shared.Data.Models;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace BeeCreak.ContentPipeline;

[ContentProcessor(DisplayName = "Animation Processor")]
public sealed class AnimationProcessor : JsonProcessor<AnimationDTO> {}