using BeeCreak.Scene.Main;
using Microsoft.Xna.Framework;

namespace BeeCreak.Shared.Data.Models;

public class Game
{
    public Game()
    {
    }

    public ICamera Camera { get; set; }

    public ITime Time { get; set; }

    public ICell ActiveCell { get; set; }
}