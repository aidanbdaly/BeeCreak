using BeeCreak.Game.Camera;
using BeeCreak.Game.Scene;
using BeeCreak.Game.Time;
using BeeCreak.Tools.Static;

namespace BeeCreak.Game.State
{
    public class GameFactory
    {
        private readonly CellFactory cellFactory;

        private readonly ISprite sprite;
        
        private readonly IEventManager events;

        public GameFactory(CellFactory cellFactory, ISprite sprite, IEventManager events)
        {
            this.cellFactory = cellFactory;
            this.sprite = sprite;
            this.events = events;
        }

        public GameDTO CreateGameDTO(Game game)
        {
            return new GameDTO
            {
                Camera = new CameraDTO
                {
                    ZoomTransform = game.Camera.ZoomTransform,
                    ViewPortWidth = game.Camera.ViewPortWidth,
                    ViewPortHeight = game.Camera.ViewPortHeight,
                    Zoom = game.Camera.Zoom,
                },
                Time = new TimeDTO
                {
                    Current = game.Time.Current,
                },
                ActiveCell = CellFactory.CreateCellDTO(game.ActiveCell),
                Name = game.Name,
            };
        }

        public Game CreateGame(GameDTO gameDTO)
        {
            return new Game(
                new Camera.Camera(sprite, events)
                {
                    ZoomTransform = gameDTO.Camera.ZoomTransform,
                    ViewPortWidth = gameDTO.Camera.ViewPortWidth,
                    ViewPortHeight = gameDTO.Camera.ViewPortHeight,
                    Zoom = gameDTO.Camera.Zoom,
                },
                new Time.Time()
                {
                    Current = gameDTO.Time.Current,
                },
                cellFactory.CreateCell(gameDTO.ActiveCell))
            {
                Name = gameDTO.Name,
            };
        }
    }
}