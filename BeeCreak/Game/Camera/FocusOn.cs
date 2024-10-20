using BeeCreak.Game.Scene.Entity;

namespace BeeCreak.Game.Objects.Camera;

public class FocusOnEvent
{
    public Entity Target { get; set; }

    public FocusOnEvent(Entity target)
    {
        Target = target;
    }
}
