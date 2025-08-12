namespace BeeCreak
{
    public class GameMountedEvent : EventArgs
    {
        public Game Game { get; }

        public GameMountedEvent(Game game)
        {
            Game = game;
        }
    }

    public class GameManager
    {
        public event EventHandler<GameMountedEvent> GameMounted;

        private readonly UserDataManager saveManager;

        private readonly SceneManager sceneManager;

        public GameManager(SceneManager sceneManager, UserDataManager saveManager)
        {
            this.sceneManager = sceneManager;
            this.saveManager = saveManager;
        }

        public Game Game { get; private set; }

        public void MountSave(string id)
        {
            if (Game != null)
            {
                if (Game.Id == id) return;

                saveManager.SaveGame(Game);
            }

            Game = saveManager.LoadGame(id);

            sceneManager.ChangeScene<GameScene>();

            GameMounted?.Invoke(this, new GameMountedEvent(Game));
        }
    }
}