using BeeCreak.Core;
using Microsoft.Xna.Framework;
using BeeCreak.App.Game.Domain.Entity;
using BeeCreak.Core.Components.Controllers;

namespace BeeCreak.App.Game
{
    public sealed class GameScene : Scene
    {
        private const int DefaultWidth = 800;

        private const int DefaultHeight = 600;

        private readonly Context context;

        private readonly SpriteController spriteController;

        private readonly EntityController entityController;

        private readonly EntityBehaviourFactory entityBehaviourFactory;

        public GameScene(Context context)
        {
            this.context = context;

            Width = DefaultWidth;
            Height = DefaultHeight;

            entityBehaviourFactory = new();
            spriteController = new(this);

            var behaviourController = new BehaviourController(this);

            entityController = new EntityController(
                entityBehaviourFactory,
                behaviourController,
                new(
                    behaviourController,
                    spriteController
                )
            );
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
                spriteController.Mount($"tile_{entry.Key.X}_{entry.Key.Y}", cell.TileMap.SpriteSheet.GetSprite(entry.Value))
            );

            cell.Entities.ForEach(e => entityController.Mount(e));
        }
    }
}
