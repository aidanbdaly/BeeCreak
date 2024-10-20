using BeeCreak.Game.Objects.Camera;
using BeeCreak.Game.Objects.Time;
using BeeCreak.Game.Scene;
using BeeCreak.Tools;

namespace BeeCreak.Game;

public class GameStateDTO
{
    public CameraDTO Camera { get; set; }
    public TimeDTO Time { get; set; }
    public CellDTO ActiveCell { get; set; }

    public GameState FromDTO(IToolCollection tools)
    {
        return new GameState(
            Camera.FromDTO(tools),
            Time.FromDTO(tools),
            ActiveCell.FromDTO(tools)
        );
    }
}
