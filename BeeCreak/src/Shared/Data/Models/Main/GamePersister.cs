using BeeCreak.Shared.Data.Config;
using Microsoft.Extensions.Options;

namespace BeeCreak.Shared.Data.Models;

public class GamePersister : GenericPersister<Game, GameDTO>
{
    public GamePersister(GenericFactory<GameDTO, Game> factory, IOptions<AppSettings> options) : base(factory, options.Value.SavePath) { }

    public new void Save(Game model, string name)
    {
        base.Save(model, name);
    }
}
