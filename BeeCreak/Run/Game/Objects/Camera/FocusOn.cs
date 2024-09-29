using BeeCreak.Run.Game.Scene.Entity;

namespace BeeCreak.Run.Game.Objects.Camera;

public class FocusOnEvent
{
    public Entity Target { get; set; }

    public FocusOnEvent(Entity target)
    {
        Target = target;
    }
}
