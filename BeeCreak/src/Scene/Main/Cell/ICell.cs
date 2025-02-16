using System.Collections.Generic;

namespace BeeCreak.Scene.Main;

public interface ICell
{
    ITileMap TileMap { get; set; }

    List<IEntity> Entities { get; set; }

    List<ILight> Lights { get; set; }

    void Bake();
}