using BeeCreak.Engine.Core;
using Newtonsoft.Json;

namespace BeeCreak
{
    public class SaveManager
    {
        private static readonly JsonSerializerSettings Settings = new()
        {
            Formatting = Formatting.Indented
        };

        public static SaveState Load(string id)
        {
            var path = Path.Combine(Game.AppDataDirectory, $"{id}.json");

            if (!File.Exists(path))
            {
                return null;
            }

            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<SaveState>(json, Settings);
        }

        public static void Save(string id, SaveState state)
        {
            var path = Path.Combine(Game.AppDataDirectory, $"{id}.json");
            var json = JsonConvert.SerializeObject(state, Settings);
            File.WriteAllText(path, json);
        }

        public static IEnumerable<string> List()
        {
            foreach (var file in Directory.EnumerateFiles(Game.AppDataDirectory, "*.json"))
            {
                yield return Path.GetFileNameWithoutExtension(file);
            }
        }
    }
}
