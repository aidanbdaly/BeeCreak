using BeeCreak.Shared.Data.Config;
using Newtonsoft.Json;

namespace BeeCreak.Shared.Services.Static;

public class GenericPersister<T, TDTO> where T : class where TDTO : class
{
    private readonly GenericFactory<TDTO, T> factory;

    private readonly string path;

    public GenericPersister(GenericFactory<TDTO, T> factory, string path)
    {
        this.factory = factory;
        this.path = path;
    }

    public void Save(T model, string name)
    {
        var dto = factory.Create(model);

        var serializedDTO = JsonConvert.SerializeObject(dto, JsonSerializerConfig.Get());

        System.IO.File.WriteAllText($"{path}/{name}.json", serializedDTO);
    }
}