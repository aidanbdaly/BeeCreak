using System.Text.Json;
using BeeCreak.Core;
using BeeCreak.App.Game.Models;

namespace BeeCreak.App.Game
{
    public class SaveManager
    {
        public static GameRecord? GetSave(string saveId)
        {
            try
            {
                using var reader = new StreamReader(Game.AppDataDirectory + $"/saves/{saveId}.json");
                var record = JsonSerializer.Deserialize<GameRecord>(reader.ReadToEnd());

                return record;
            }
            catch
            {
                return null;
            }
        }
    }
}