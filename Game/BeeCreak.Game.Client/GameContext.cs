using BeeCreak.Engine;

namespace BeeCreak.Game
{
    public class GameContext(App app)
    {
        public string SaveId { get; set; } = string.Empty;
        
    }
}