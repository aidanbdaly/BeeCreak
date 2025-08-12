using Newtonsoft.Json;

namespace BeeCreak
{
    public class UserDataManager
    {
        private static string AppDataDirectory =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BeeCreak");

        private readonly JsonSerializer jsonSerializer = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };

        private readonly GameFactory gameFactory;

        public UserDataManager(GameFactory gameFactory)
        {
            this.gameFactory = gameFactory;

            if (!Directory.Exists(AppDataDirectory))
            {
                Directory.CreateDirectory(AppDataDirectory);
            }
        }

        public Game LoadGame(string id)
        {
            var gamePath = Path.Combine(AppDataDirectory, $"{id}.json");

            if (!File.Exists(gamePath))
            {
                return gameFactory.Create(id);
            }
            
            using var streamReader = new StreamReader($"{AppDataDirectory}/{id}.json");
            using var reader = new JsonTextReader(streamReader);

            return gameFactory.Create(id, jsonSerializer.Deserialize<GameState>(reader));
        }

        public void SaveGame(Game game)
        {
            using var streamWriter = new StreamWriter($"{AppDataDirectory}/{game.Id}.json");
            using var writer = new JsonTextWriter(streamWriter);

            jsonSerializer.Serialize(writer, game.State);
        }

        public static IEnumerable<string> ListSaves()
        {
            foreach (var file in Directory.EnumerateFiles(AppDataDirectory, "*.json"))
            {
                yield return Path.GetFileNameWithoutExtension(file);
            }
        }

        public UserSettings LoadUserSettings()
        {
            var settingsPath = Path.Combine(AppDataDirectory, "settings.json");

            if (!File.Exists(settingsPath))
            {
                return new UserSettings();
            }

            using var streamReader = new StreamReader(settingsPath);
            using var reader = new JsonTextReader(streamReader);

            return jsonSerializer.Deserialize<UserSettings>(reader) ?? new UserSettings();
        }

        public void SaveUserSettings(UserSettings settings)
        {
            var settingsPath = Path.Combine(AppDataDirectory, "settings.json");

            using var streamWriter = new StreamWriter(settingsPath);
            using var writer = new JsonTextWriter(streamWriter);

            jsonSerializer.Serialize(writer, settings);
        }
    }
}
