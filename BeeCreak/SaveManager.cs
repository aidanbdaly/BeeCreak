using System.Collections.Generic;
using System.Linq;
using BeeCreak.Game.Objects.Camera;
using BeeCreak.Game.Objects.Time;
using BeeCreak.Game.Scene;
using BeeCreak.Game.Scene.Entity;
using BeeCreak.Game.Scene.Entity.Instances.Character;
using BeeCreak.Game.Scene.Entity.Instances.Creature;
using BeeCreak.Game.Scene.Light;
using BeeCreak.Tools;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace BeeCreak.Game;

public class SaveManager
{
    private JsonSerializerSettings SerializerSettings { get; set; }
    private readonly string SaveDirectory = "Saves";
    private readonly IToolCollection Tools;

    public SaveManager(IToolCollection tools)
    {
        Tools = tools;

        SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };
    }

    public List<string> GetSaves()
    {
        return System.IO.Directory.GetFiles(SaveDirectory).ToList();
    }

    public void Save(GameState gameState)
    {
        var dto = gameState.ToDTO();

        System.IO.File.WriteAllText(
            $"{SaveDirectory}/save.json",
            JsonConvert.SerializeObject(dto, SerializerSettings)
        );
    }

    public GameState Load(string name)
    {
        var save = System.IO.File.ReadAllText($"{SaveDirectory}/{name}");

        var dto = JsonConvert.DeserializeObject<GameStateDTO>(save, SerializerSettings);

        return dto.FromDTO(Tools);
    }

    public GameState New()
    {
        var camera = new Camera(Tools);
        var time = new Time();

        var lights = new List<Light> { new(Tools, new Vector2(150, 150), 5, 1.5f, 5), };

        var entities = new List<Entity>
        {
            new Character(
                Tools,
                new Vector2(150 * Tools.Static.TILE_SIZE, 150 * Tools.Static.TILE_SIZE)
            ),
            new Creature(
                Tools,
                new Vector2(150 * Tools.Static.TILE_SIZE, 150 * Tools.Static.TILE_SIZE)
            ),
        };

        var cell = new Cell(Tools, lights, entities, new Vector2(150, 150), 300, "Test");

        return new GameState(camera, time, cell);
    }
}
