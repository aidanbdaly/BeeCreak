using BeeCreak.Scene.Main;

namespace BeeCreak.Shared.Data.Models;

public class GameDTO
{
    public CameraDTO Camera { get; set; } = new();

    public TimeDTO Time { get; set; } = new();

    public string ActiveCellName { get; set; }
}
