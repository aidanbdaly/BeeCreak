namespace BeeCreak.Game.Camera
{
    using global::BeeCreak.Game.Scene.Entity;

    public class FocusOnEvent
    {
        public FocusOnEvent(Entity target)
        {
            Target = target;
        }

        public Entity Target { get; set; }
    }
}
