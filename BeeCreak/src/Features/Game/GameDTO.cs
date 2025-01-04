namespace BeeCreak.Game
{
    using global::BeeCreak.Game.Camera;
    using global::BeeCreak.Game.Scene;
    using global::BeeCreak.Game.Time;

    public class GameDTO
    {
        public CameraDTO Camera { get; set; }

        public TimeDTO Time { get; set; }

        public CellDTO ActiveCell { get; set; }

        public string Name { get; set; }
    }
}