using BeeCreak.Core.Components;
using BeeCreak.Core;
using Microsoft.Xna.Framework;
using BeeCreak.App.Game.Domain.Entity;

namespace BeeCreak.App.Game
{
    public sealed class GameScene : Scene
    {
        private const int DefaultWidth = 800;

        private const int DefaultHeight = 600;

        private readonly Context context;

        private readonly BehaviourFactory behaviours;

        public GameScene(Context context)
        {
            this.context = context;

            Width = DefaultWidth;
            Height = DefaultHeight;

            behaviours = new BehaviourFactory();
        }

        public void Initialize()
        {
            behaviours.Register(Behaviour.Control, ctx => new ControlBehaviour(context.inputManager, ctx.TileMap, ctx.Entity));
        }

        public override void LoadContent()
        {
            var game = context.content.Load<GameRecord>("Game/default");

            var cell = game.ActiveCell;

            cell.TileMap.Tiles.ToList().ForEach((entry) =>
            {
                var sprite = new Sprite(cell.TileMap.SpriteSheet)
                {
                    Position = new Vector2(
                        entry.Key.X * 32,
                        entry.Key.Y * 32
                    )
                };
                sprite.SetSprite(entry.Value);

                AddComponent(sprite);
            });

            cell.Entities.ForEach(e =>
            {
                e.Base.Behaviours.ForEach(b =>
                {
                    AddComponent(behaviours.Create(b, new(cell.TileMap, e)));
                });

                AddComponent(new Animation(e.Base.AnimationSheet));
            });
        }
    }
}
