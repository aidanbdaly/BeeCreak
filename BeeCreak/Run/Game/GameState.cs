using BeeCreak.Run.Game.Objects.Camera;
using BeeCreak.Run.Game.Objects.Time;
using BeeCreak.Run.Game.Scene;

namespace BeeCreak.Run.Game;

public class GameState
{
    public ICamera Camera { get; set; }
    public ITime Time { get; set; }
    public ICell ActiveCell { get; set; }

    public GameState(Camera camera, Time time, ICell currentCell)
    {
        Camera = camera;
        Time = time;
        ActiveCell = currentCell;
    }

    public GameStateDTO ToDTO()
    {
        return new GameStateDTO
        {
            Camera = Camera.ToDTO(),
            Time = Time.ToDTO(),
            ActiveCell = ActiveCell.ToDTO()
        };
    }
}
