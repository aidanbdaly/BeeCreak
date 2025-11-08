namespace BeeCreak.Core.Transitions
{
    public interface ITransition
    {
        Task PlayAsync(CancellationToken ct);
    }
}
