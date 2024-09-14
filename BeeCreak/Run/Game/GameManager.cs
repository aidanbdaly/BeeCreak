using System.Collections.Generic;
using BeeCreak.Run.GameObjects.World;
using BeeCreak.Run.GameObjects.World.Entity;
using BeeCreak.Run.GameObjects.World.Entity.Events;
using BeeCreak.Run.GameObjects.World.Light;
using BeeCreak.Run.Generation;
using BeeCreak.Run.UI;
using Microsoft.Xna.Framework;

namespace BeeCreak.Run.GameObjects;

public class GameManager : IDynamicDrawable
{
    private readonly CellManager CellManager;
    private readonly UIManager UI;
    private readonly IToolCollection Tools;
    private readonly IEventManager EventManager;

    public GameManager(IToolCollection tools, IEventManager eventBus)
    {
        Tools = tools;
        EventManager = eventBus;

        var cell = new Cell(tools, eventBus, "Test", 300, new Vector2(150, 150))
        {
            Lights = new List<ILight> { new Light(tools, new Vector2(150, 150), 5, 1.5f, 5), },
        };

        CellManager = new CellManager(tools, eventBus, cell);
        UI = new UIManager(tools, eventBus);

        EventManager.Dispatch(new AddEntityEvent(new Character(Tools, EventManager), "Test"));
        EventManager.Dispatch(new AddEntityEvent(new Creature(Tools), "Test"));
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
