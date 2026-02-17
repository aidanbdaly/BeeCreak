namespace BeeCreak.Game.Orchestrator.Models;

public sealed class CreateServerRequest
{
    public string? Image { get; set; }
    public int? ContainerPort { get; set; }
    public bool? ExposeNodePort { get; set; }
    public List<string>? Args { get; set; }
    public Dictionary<string, string>? Metadata { get; set; }
}
