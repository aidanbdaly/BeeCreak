using BeeCreak.Core.Models;

namespace BeeCreak.Core.Components.Controllers
{
    public class AnimationController(
        BehaviourController behaviourController,
        SpriteController spriteController
        )
    {
        private readonly BehaviourController behaviourController = behaviourController;

        private readonly SpriteController spriteController = spriteController;

        public TextureNode Mount(
            string id,
            Animation animation,
            int duration)
        {
            var ticker = new Ticker(duration / animation.Frames.Count);

            var node = spriteController.Mount($"animation_{id}", animation.GetSprite(0));

            behaviourController.Mount($"animation_ticker_{id}", ticker);

            node.SourceRectangle = animation.Frames[0].ToRectangle();

            ticker.OnTick += () =>
            {
                node.SourceRectangle = animation.Frames[frames.IndexOf(sprite.SourceRectangle) + 1];
            };

            return node;
        }

        public void Unmount(string id)
        {
            behaviourController.Unmount(id);
            spriteController.Unmount(id);
        }
    }
}