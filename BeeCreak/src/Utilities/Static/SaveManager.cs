using System.Collections.Generic;
using System.Linq;
using BeeCreak.Config;
using BeeCreak.Game.Scene;
using BeeCreak.Game.Scene.Entity;
using BeeCreak.Game.Scene.Entity.Instances.Character;
using BeeCreak.Game.Scene.Entity.Instances.Creature;
using BeeCreak.Game.Scene.Light;
using BeeCreak.Game.State;
using BeeCreak.Tools.Dynamic.Input;
using BeeCreak.Tools.Static;
using BeeCreak.Utilities.Static;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace BeeCreak.Game;

public class SaveManager : ISaveManager
{
    private readonly JsonSerializerSettings serializerSettings;

    private readonly GameFactory gameFactory;

    private readonly ISprite sprite;

    private readonly IInput input;

    private readonly IEventManager events;

    private readonly IShapeRouter shapeRouter;

    public SaveManager(
        ISprite sprite,
        IInput input,
        GameFactory gameFactory,
        IEventManager events,
        IShapeRouter shapeRouter
        )
    {
        this.sprite = sprite;
        this.input = input;
        this.events = events;
        this.gameFactory = gameFactory;
        this.shapeRouter = shapeRouter;

        serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };
    }

    public List<string> GetSaves()
    {
        return System.IO.Directory.GetFiles(Globals.SAVE_DIRECTORY).Select(x => x.Split('/').Last()).ToList();
    }

    public void Save(Game game)
    {
        var saveDTO = gameFactory.CreateGameDTO(game);

        System.IO.File.WriteAllText(
            $"{Globals.SAVE_DIRECTORY}/{game.Name}.json",
            JsonConvert.SerializeObject(saveDTO, serializerSettings));
    }

    public Game Load(string name)
    {
        var save = System.IO.File.ReadAllText($"{Globals.SAVE_DIRECTORY}/{name}");

        var dto = JsonConvert.DeserializeObject<GameDTO>(save, serializerSettings);

        return gameFactory.CreateGame(dto);
    }

    public Game New()
    {
        var cellSize = 300;

        var camera = new Camera.Camera(sprite, events);
        var time = new Time.Time();

        var tileMap = shapeRouter.Route(cellSize);

        var lights = new List<ILight>()
            {
                new Light(sprite)
                {
                    Position = new Vector2(150, 150),
                    Radius = 10,
                    MaxScale = 1.5f,
                    Period = 5,
                },
            };

        var entities = new List<IEntity>
            {
                new Character(
                    input,
                    events)
                    {
                        WorldPosition = new Vector2(150 * Globals.TileSize, 150 * Globals.TileSize),
                    },
                new Creature(
                    input)
                    {
                        WorldPosition = new Vector2(150 * Globals.TileSize, 150 * Globals.TileSize),
                    },
            };

        var cell = new Cell(lights, entities, tileMap, cellSize, "Test");

        return new Game(camera, time, cell);
    }
}