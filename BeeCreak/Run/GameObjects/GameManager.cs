using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World;
using BeeCreak.Run.GameObjects.World.Entity;
using BeeCreak.Run.GameObjects.World.Light;
using BeeCreak.Run.Generation;
using BeeCreak.Run.UI;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects;

public class GameManager : IGameObject
{
    private readonly CellManager CellManager;
    private readonly UIManager UI;
    private readonly IToolCollection Tools;
    private readonly IEventBus EventBus;

    public GameManager(IToolCollection tools, IEventBus eventBus)
    {
        Tools = tools;
        EventBus = eventBus;

        ShapeRouter shapeRouter = new(tools, 12, 300);

        var cell = new Cell()
        {
            Size = 300,
            SpawnPoint = new Vector2(150, 150),
            Entities = new List<IEntity>
            {
                new Character(tools, eventBus, new Vector2(150, 150)),
                new Creature(tools, new Vector2(150, 150)),
            },
            Lights = new List<ILight> { new Light(tools, new Vector2(150, 150), 5, 1.5f, 5), },
            Map = shapeRouter.Route(),
        };

        CellManager = new CellManager(tools, eventBus, cell);
        UI = new UIManager(tools, eventBus);
    }

    public void Update(GameTime gameTime)
    {
        CellManager.Update(gameTime);
        UI.Update(gameTime);
    }

    public void Draw()
    {
        CellManager.Draw();
        UI.Draw();
    }
}
