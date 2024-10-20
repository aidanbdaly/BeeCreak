using BeeCreak.Game;
using BeeCreak.Game.Objects.Camera;
using BeeCreak.Game.Objects.Time;
using BeeCreak.Game.Scene;

public interface IGameState
{
    ICamera Camera { get; set; }
    ITime Time { get; set; }
    ICell ActiveCell { get; set; }
    GameStateDTO ToDTO();
}
