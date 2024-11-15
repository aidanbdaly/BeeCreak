namespace BeeCreak.Game
{
    using System.Collections.Generic;
    using System.Linq;
    using global::BeeCreak.Config;
    using global::BeeCreak.Game.Scene;
    using global::BeeCreak.Game.Scene.Entity;
    using global::BeeCreak.Game.Scene.Entity.Instances.Character;
    using global::BeeCreak.Game.Scene.Entity.Instances.Creature;
    using global::BeeCreak.Game.Scene.Light;
    using global::BeeCreak.Game.State;
    using global::BeeCreak.Generation;
    using global::BeeCreak.Tools.Dynamic.Input;
    using global::BeeCreak.Tools.Static;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Newtonsoft.Json;

    public class SaveManager : ISaveManager
    {
        private readonly string saveDirectory = "Saves";

        private readonly JsonSerializerSettings serializerSettings;

        private readonly GameFactory gameFactory;

        private readonly ISprite sprite;

        private readonly IInput input;

        private readonly IEventManager events;

        public SaveManager(
            ISprite sprite,
            IInput input,
            IEventManager events)
        {
            this.sprite = sprite;
            this.input = input;
            this.events = events;

            serializerSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.Indented,
            };
        }

        public List<string> GetSaves()
        {
            return System.IO.Directory.GetFiles(saveDirectory).ToList();
        }

        public void Save(Game game)
        {
            var saveDTO = gameFactory.CreateGameDTO(game);

            System.IO.File.WriteAllText(
                $"{saveDirectory}/save.json",
                JsonConvert.SerializeObject(saveDTO, serializerSettings));
        }

        public Game Load(string name)
        {
            var save = System.IO.File.ReadAllText($"{saveDirectory}/{name}");

            var dto = JsonConvert.DeserializeObject<GameDTO>(save, serializerSettings);

            return gameFactory.CreateGame(dto);
        }

        public Game New()
        {
            var camera = new Camera.Camera(sprite, events);
            var time = new Time.Time();

            var shapeRouter = new ShapeRouter(sprite);

            var tileMap = shapeRouter.Route(300);

            var lights = new ILight[300, 300];

            lights[150, 150] = new Light(sprite)
            {
                Radius = 5,
                MaxScale = 1.5f,
                Period = 5,
            };

            var lightMap = new LightMap(lights);

            var entities = new List<Entity>
            {
                new Character(
                    sprite,
                    input,
                    events)
                    {
                        WorldPosition = new Vector2(150 * Globals.TileSize, 150 * Globals.TileSize),
                    },
                new Creature(
                    sprite,
                    input)
                    {
                        WorldPosition = new Vector2(150 * Globals.TileSize, 150 * Globals.TileSize),
                    },
            };

            var cell = new Cell(lightMap, entities, tileMap, "Test");

            return new Game(camera, time, cell);
        }
    }
}