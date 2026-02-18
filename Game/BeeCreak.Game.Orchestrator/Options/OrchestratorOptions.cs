namespace BeeCreak.Game.Orchestrator.Options;

public sealed class OrchestratorOptions
{
    public string Namespace { get; set; } = "default";
    public string ServerImage { get; set; } = "ghcr.io/aidanbdaly/beecreak-game-server:latest";
    public int ContainerPort { get; set; } = 7777;
    public int HealthPort { get; set; } = 8080;
    public bool ExposeNodePort { get; set; } = true;
    public string HeartbeatPath { get; set; } = "/heartbeat";
    public int HeartbeatInitialDelaySeconds { get; set; } = 10;
    public int HeartbeatPeriodSeconds { get; set; } = 10;
    public string RestartPolicy { get; set; } = "Never";

    public string LabelSelector { get; set; } = "beecreak.orchestrator=true";
    public string OrchestratorLabelKey { get; set; } = "beecreak.orchestrator";
    public string OrchestratorLabelValue { get; set; } = "true";
    public string ServerIdLabelKey { get; set; } = "beecreak.serverId";
    public string AppLabelKey { get; set; } = "app";
    public string AppLabelValue { get; set; } = "beecreak-gameserver";

    public string MetadataAnnotationKey { get; set; } = "beecreak/metadata";
    public string ImageAnnotationKey { get; set; } = "beecreak/image";
    public string ArgsAnnotationKey { get; set; } = "beecreak/args";
    public string CreatedAtAnnotationKey { get; set; } = "beecreak/createdAt";

    public string PreferredNodeAddressType { get; set; } = "InternalIP";
    public string ApiKey { get; set; } = "change-me";
    public string ApiKeyHeader { get; set; } = "X-Api-Key";

    public List<string> DefaultArgs { get; set; } = new()
    {
        "--port",
        "{port}",
        "--session-id",
        "{serverId}",
        "--health-port",
        "{healthPort}",
        "--heartbeat-path",
        "{heartbeatPath}"
    };

    public string? KubeConfigPath { get; set; }
    public string? KubeContext { get; set; }
}
