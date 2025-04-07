using Newtonsoft.Json;

public class SaveManager
{
    private readonly SaveFactory saveFactory;

    private readonly JsonSerializer jsonSerializer;

    public SaveManager()
    {
        jsonSerializer = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore
        };
    }

    public GameState GetSave(string id)
    {
        GameStateDTO save;

        using var streamReader = new StreamReader(id);
        using var reader = new JsonTextReader(streamReader);
        {
            save = jsonSerializer.Deserialize<GameStateDTO>(reader);
        }

        return saveFactory.fromDTO(save);
    }

    public void Save(GameStateDTO gameState, string id)
    {
        using var streamWriter = new StreamWriter(id);
        using var writer = new JsonTextWriter(streamWriter);
        
        jsonSerializer.Serialize(writer, gameState);
    }
}