using System.Collections.Generic;
using System.Linq;
using BeeCreak.Shared.Data.Config;
using Newtonsoft.Json;

namespace BeeCreak.Shared.Services.Static;

public class GenericProvider<T, TDTO> : IGenericProvider<T> where T : class
{
    public GenericProvider(GenericFactory<T, TDTO> factory, string path)
    {
        var serializedModel = System.IO.File.ReadAllText(path);

        var DTODictionary = JsonConvert.DeserializeObject<Dictionary<string, TDTO>>(serializedModel, JsonSerializerConfig.Get());

        Dictionary = DTODictionary.Select((entry) => new KeyValuePair<string, T>(
                entry.Key,
                factory.Create(entry.Value)
            )).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    private Dictionary<string, T> Dictionary { get; set; }

    public T Get(string name)
    {
        return Dictionary[name];
    }
}