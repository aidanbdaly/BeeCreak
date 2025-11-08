using BeeCreak.App.Game.Models;

namespace BeeCreak.App.Game
{
    public record GameRecord
    (
        string ActiveCellId,
        EntityRecord Player
    );
}
