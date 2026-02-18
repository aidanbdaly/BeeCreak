namespace BeeCreak.Orchestrator.Models;

public sealed class GameServerDto
{
    public string Id { get; set; } = string.Empty;
    public GameServerStatus Status { get; set; } = GameServerStatus.Unknown;
    public DateTimeOffset CreatedAt { get; set; }
    public string? Image { get; set; }
    public int? ContainerPort { get; set; }
    public string? Endpoint { get; set; }
    public int? NodePort { get; set; }
    public string? PodName { get; set; }
    public string? ServiceName { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
}
