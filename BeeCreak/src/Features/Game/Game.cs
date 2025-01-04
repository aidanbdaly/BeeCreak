using BeeCreak.Game.Camera;
using BeeCreak.Game.Scene;
using BeeCreak.Game.Time;

namespace BeeCreak.Game;

public class Game
{
    public Game(ICamera camera, ITime time, ICell currentCell)
    {
        Camera = camera;
        Time = time;
        ActiveCell = currentCell;
    }

    public Game()
    {
    }

    public ICamera Camera { get; set; }

    public ITime Time { get; set; }

    public ICell ActiveCell { get; set; }

    public string Name { get; set; }
}

