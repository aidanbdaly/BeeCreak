namespace BeeCreak.Engine.Transitions
{
    public interface ITransition
    {
        Task PlayAsync(CancellationToken ct);
    }
}
