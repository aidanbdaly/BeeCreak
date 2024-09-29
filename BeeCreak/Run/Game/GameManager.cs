using System;
using System.Collections.Generic;
using BeeCreak.Run.Game.Objects.Camera;
using BeeCreak.Run.Game.Objects.Time;
using BeeCreak.Run.Game.Scene;
using BeeCreak.Run.Game.Scene.Entity;
using BeeCreak.Run.Game.Scene.Entity.Instances.Character;
using BeeCreak.Run.Game.Scene.Entity.Instances.Creature;
using BeeCreak.Run.Game.Scene.Light;
using BeeCreak.Run.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace BeeCreak.Run.Game;

public class GameManager : IDynamicDrawable
{
    public ITime Time { get; set; }
    public ICamera Camera { get; set; }
    public ICell ActiveCell { get; set; }
    private CellManager CellManager;

    //private UIManager UIManager;

    private readonly JsonSerializerSettings SerializerSettings =
        new() { TypeNameHandling = TypeNameHandling.All, Formatting = Formatting.Indented, };
    private readonly string SaveDirectory = "Saves";
    private IToolCollection Tools;

    public GameManager(IToolCollection tools)
    {
        Tools = tools;

        if (!System.IO.Directory.Exists(SaveDirectory))
        {
            System.IO.Directory.CreateDirectory(SaveDirectory);

            New();

            Save();
        }
        else
        {
            Load();
        }
    }

    private GameStateDTO ToDTO()
    {
        return new GameStateDTO
        {
            ActiveCell = ActiveCell.ToDTO(),
            Camera = Camera.ToDTO(),
            Time = Time.ToDTO()
        };
    }

    private void New()
    {
        Camera = new Camera(Tools);
        Time = new Time();

        ActiveCell = new Cell(Tools, "Test", 300, new Vector2(150, 150))
        {
            Lights = new List<Light> { new(Tools, new Vector2(150, 150), 5, 1.5f, 5), },
            Entities = new List<Entity>
            {
                new Character(
                    Tools,
                    new Vector2(150 * Tools.Static.TILE_SIZE, 150 * Tools.Static.TILE_SIZE)
                ),
                new Creature(
                    Tools,
                    new Vector2(150 * Tools.Static.TILE_SIZE, 150 * Tools.Static.TILE_SIZE)
                ),
            }
        };

        ActiveCell.Initialize();

        CellManager = new CellManager(Tools, ActiveCell);

        //UIManager = new UIManager(Tools);
    }

    private void Load()
    {
        var save = System.IO.File.ReadAllText($"{SaveDirectory}/save.json");

        var dto = JsonConvert.DeserializeObject<GameStateDTO>(save, SerializerSettings);

        var GameState = dto.FromDTO(Tools);
        Camera = GameState.Camera;
        ActiveCell = GameState.ActiveCell;
        Time = GameState.Time;

        Console.WriteLine(ActiveCell.Entities.Count);

        ActiveCell.Initialize();

        CellManager = new CellManager(Tools, ActiveCell);
    }

    private void Save()
    {
        var dto = ToDTO();

        System.IO.File.WriteAllText(
            $"{SaveDirectory}/save.json",
            JsonConvert.SerializeObject(dto, SerializerSettings)
        );
    }

    public void Update(GameTime gameTime)
    {
        if (Tools.Dynamic.Input.OnKeyClick(Keys.J))
        {
            Console.WriteLine("Saving");
            Save();

            Console.WriteLine("Saved");
        }

        CellManager.Update(gameTime);
        Camera.Update(gameTime);

        //UIManager.Update(gameTime);
    }

    public void Draw()
    {
        CellManager.Draw(Camera);
        //UIManager.Draw();
    }
}
