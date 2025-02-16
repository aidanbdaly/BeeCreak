using Newtonsoft.Json;

namespace BeeCreak.Shared.Data.Config;

public static class JsonSerializerConfig
{
    public static JsonSerializerSettings Get()
    {
        return new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        };
    }
}