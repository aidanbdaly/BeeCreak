using Newtonsoft.Json;

public class GameManager
{
    private readonly JsonSerializer jsonSerializer = new()
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore
    };

    public GameManager() {}

    public GameState LoadOrCreate(string id)
    {
        GameState save;

        try
        {
            using var streamReader = new StreamReader(id);
            using var reader = new JsonTextReader(streamReader);
            {
                save = jsonSerializer.Deserialize<GameState>(reader);
            }

            streamReader.Close();
            streamReader.Dispose();
            reader.Close();
        }
        catch (FileNotFoundException)
        {
            save = GameState.Default();

            Save(save, id);
        }

        return save;
    }

    public void Save(GameState gameState, string id)
    {
        using var streamWriter = new StreamWriter($"src/Saves/{id}.json");
        using var writer = new JsonTextWriter(streamWriter);

        jsonSerializer.Serialize(writer, gameState);

        writer.Flush();
        streamWriter.Flush();
        streamWriter.Dispose();
    }
}