using BeeCreak.Core.Components;
using BeeCreak.Core;
using BeeCreak.App.Game.Models;
using BeeCreak.App.Game.Domain.Tile;

namespace BeeCreak.App.Game
{
    public sealed class GameScene : Scene
    {
        private const int DefaultWidth = 800;

        private const int DefaultHeight = 600;

        private const string DefaultSaveId = "default";

        private readonly Context context;

        private readonly ComponentFactory componentFactory;

        public GameScene(Context context)
        {
            this.context = context;

            Width = DefaultWidth;
            Height = DefaultHeight;

            componentFactory = new ComponentFactory(context);
        }

        public override void LoadContent()
        {
            var saveId = string.IsNullOrWhiteSpace(context.SaveId) ? DefaultSaveId : context.SaveId;

            var save = SaveManager.GetSave(saveId);

            save ??= context.content.Load<GameRecord>("Game/default");

            var cell = context.content.Load<CellRecord>(save.ActiveCellId);

            var tileMap = new TileMap(cell.Value.Tiles);

            cell.Value.Tiles.ForEach(t => 
                AddComponent(componentFactory.Sprite(t.Id))
            );

            cell.Value.Entities.ForEach(e =>
            {
                AddComponent(entityFactory.CreateEntity(e, tileMap));
            });
        }
    }
}
