using BeeCreak.Shared.Data.Config;
using Microsoft.Extensions.Options;

namespace BeeCreak.Shared.Data.Models;

public class GameProvider : GenericProvider<Game, GameDTO>
{
    public GameProvider(GenericFactory<Game, GameDTO> factory, IOptions<AppSettings> options) : base(factory, options.Value.SavePath) { }

    public Game GetSave(string name)
    {
        return Get(name);
    }
}