using BeeCreak.Run.Game.Objects.Camera;
using BeeCreak.Run.Game.Objects.Time;
using BeeCreak.Run.Game.Scene;
using BeeCreak.Run.Tools;

namespace BeeCreak.Run.Game;

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
