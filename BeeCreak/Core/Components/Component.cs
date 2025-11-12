namespace BeeCreak.Core.Components
{
    public class Component : IComponent
    {
        public bool IsEnabled { get; set; } = true;

        public virtual void Initialize() { }
    }
}