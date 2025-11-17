using BeeCreak.Core;
using BeeCreak.Game.Domain.Entity;
using BeeCreak.Core.Input;
using BeeCreak.Core.Components;
using BeeCreak.Core.State;
using Microsoft.Xna.Framework;

namespace BeeCreak.Game
{
    public sealed class GameScene : Scene
    {
        private const int DefaultWidth = 800;

        private const int DefaultHeight = 600;

        private readonly App app;

        private readonly EntityService entityService;

        private readonly ActionBuffer bindings;

        public GameScene(App app) : base(app.GraphicsDevice, DefaultWidth, DefaultHeight)
        {
            this.app = app;

            entityService = new EntityService(this, app.Services.GetService<InputManager>());
        }

        public override void LoadContent()
        {
            var game = app.Content.Load<GameRecord>("Game/default");

            var cell = game.ActiveCell;

            // TileMapLoader
            foreach (var tile in cell.TileMap.Enumerate())
            {
                var texture = new TextureComponent(cell.TileMap.SpriteSheet.Texture, tile.Position.ToVector2());

                bindings.Enqueue(texture.SourceRectangle.Listen(tile.State.SpriteName, (spriteName) => cell.TileMap.SpriteSheet.Frames[spriteName].ToRectangle()));


                bindings.Enqueue(AddComponent(texture));
            }

            cell.Entities.ForEach(e => entityService.Load(e));
        }

        public void Dispose()
        {
            bindings.Flush();
        }
    }
}
