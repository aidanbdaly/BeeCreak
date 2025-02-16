using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BeeCreak.Scene.Main;

public class EntityController : IEntityController
{
    public EntityController() {}

    private List<IEntity> Entities { get; set; }

    public void Load(List<IEntity> entities)
    {
        Entities = entities;
    }

    public void Update(GameTime gameTime)
    {
        foreach (var entity in Entities)
        {
            entity.Update(gameTime);
        }
    }

    public void Draw()
    {
        foreach (var entity in Entities)
        {
            entity.Draw();
        }
    }
}