using System.Threading.Tasks;
using BeeCreak.Engine.Core;

namespace BeeCreak
{
    public class GameMountedEvent : EventArgs
    {
        public GameContext Game { get; }

        public GameMountedEvent(GameContext game)
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

        public GameContext Game { get; private set; }

        public async Task MountSave(string id)
        {
            if (Game != null)
            {
                if (Game.Id == id) return;

                saveManager.SaveGame(Game);
            }

            Game = saveManager.LoadGame(id);

            await sceneManager.ChangeSceneAsync("GameScene");

            GameMounted?.Invoke(this, new GameMountedEvent(Game));
        }
    }
}