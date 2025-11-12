namespace BeeCreak.Core.Components
{
    public interface IComponent
    {
        bool IsEnabled { get; set; }

        void Initialize();
    }
}