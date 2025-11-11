using BeeCreak.Core.Models;
using Microsoft.Xna.Framework;

namespace BeeCreak.Core.Components
{
    public class Animation(AnimationSheet animationSheet) : Texture(animationSheet.Texture), IUpdateable
    {
        private readonly AnimationSheet animationSheet = animationSheet;

        private List<Rectangle> Frames { get; set; } = [];

        private float Speed { get; set; } = 1f;

        public void SetAnimation(string animationName)
        {
            if (animationSheet.Animations.TryGetValue(animationName, out var frames))
            {
                Frames = frames;
            }
            else
            {
                throw new ArgumentException($"Animation '{animationName}' not found in animation sheet.");
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Frames.Count == 0)
            {
                return;
            }

            var totalDuration = Frames.Count / Speed;
            var time = (float)gameTime.TotalGameTime.TotalSeconds % totalDuration;
            var currentFrameIndex = (int)(time * Speed) % Frames.Count;

            SourceRectangle = Frames[currentFrameIndex];
        }
    }
}