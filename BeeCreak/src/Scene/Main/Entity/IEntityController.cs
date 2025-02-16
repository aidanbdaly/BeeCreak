using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public interface IEntityController
{
    void Load(List<IEntity> entities);

    void Update(GameTime gameTime);

    void Draw();
}
