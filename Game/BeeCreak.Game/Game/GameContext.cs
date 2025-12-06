using BeeCreak.Engine;

namespace BeeCreak.Game
{
    public class GameContext(App app)
    {
        public string SaveId { get; set; } = string.Empty;

        // Game represents the initial state of the game, i.e when state has no entry, the truth is found here
        public GameRecord Game { get; } = app.Content.Load<GameRecord>("Game/default");
    }
}