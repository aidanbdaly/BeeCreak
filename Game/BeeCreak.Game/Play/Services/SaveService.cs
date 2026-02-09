using BeeCreak.Engine;

namespace BeeCreak.Game.Services
{
    public interface ISaveService
    {
        GameRecord Game { get; }
    }

    public class SaveService(App app) : ISaveService
    {
        // Game represents the initial state of the game, i.e when state has no entry, the truth is found here
        public GameRecord Game { get; } = app.Content.Load<GameRecord>("Game/default");
    }
}