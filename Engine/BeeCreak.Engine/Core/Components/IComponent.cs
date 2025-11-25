namespace BeeCreak.Core.Components
{
    public interface IComponent
    {
        Guid Id { get; }
        
        bool IsEnabled { get; set; }
    }
}