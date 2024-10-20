namespace BeeCreak.Game
{
    using global::BeeCreak.Game.Objects.Camera;
    using global::BeeCreak.Game.Objects.Time;
    using global::BeeCreak.Game.Scene;

    public class GameState
    {
        public GameState(ICamera camera, ITime time, ICell currentCell)
        {
            Camera = camera;
            Time = time;
            ActiveCell = currentCell;
        }

        public GameState()
        {
        }

        public ICamera Camera { get; set; }

        public ITime Time { get; set; }

        public ICell ActiveCell { get; set; }

        public GameStateDTO ToDTO()
        {
            return new GameStateDTO
            {
                Camera = Camera.ToDTO(),
                Time = Time.ToDTO(),
                ActiveCell = ActiveCell.ToDTO(),
            };
        }
    }
}
