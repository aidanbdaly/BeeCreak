using BeeCreak.Orchestrator.Models;

namespace BeeCreak.Orchestrator.Services;

public interface IGameServerOrchestrator
{
    Task<IReadOnlyList<GameServerDto>> ListAsync(CancellationToken cancellationToken);
    Task<GameServerDto?> GetAsync(string id, CancellationToken cancellationToken);
    Task<GameServerDto> CreateAsync(CreateServerRequest request, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
}
