using BeeCreak.App.Game.Models;

namespace BeeCreak.App.Game.Domain
{
    public class GameContext(
        GameRecord Record
    )
    {
        public GameRecord Game => Record;      
    }

}