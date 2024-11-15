namespace BeeCreak.Game
{
    using global::BeeCreak.Game.Camera;
    using global::BeeCreak.Game.Scene;
    using global::BeeCreak.Game.Time;

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
    }
}
