using BeeCreak.Core;
using BeeCreak.Core.Input;
using BeeCreak.Game.Cell.Tile;
using BeeCreak.Game.Domain.Entity;
using BeeCreak.Game.Models;

namespace BeeCreak.Game.Cell
{
    public class CellManager(Scene scene, InputManager input)
    {
        private readonly EntityFactory entityFactory = new(input);

        private void Load(CellReference cell)
        {
            var tileMap = TileMapFactory.Create(cell.TileMap);

            scene.AddComponent(tileMap);

            cell.Entities.ForEach(e =>
            {
                scene.AddComponent(entityFactory.Create(e));
            });
        }

        public void ChangeCell(CellReference cell)
        {
            //unload

            Load(cell);
        }
    }
}