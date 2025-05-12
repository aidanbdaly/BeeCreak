using Microsoft.Xna.Framework;

public class SystemManager {
    private readonly List<Action<GameTime>> systems = new List<Action<GameTime>>();

    public SystemManager() {}

    public void AddSystem(Action<GameTime> system) {
        systems.Add(system);
    }

    public void RemoveSystem(Action<GameTime> system) {
        systems.Remove(system);
    }

    public void Update(GameTime gameTime) {
        foreach (var system in systems) {
            system.Invoke(gameTime);
        }
    }
}