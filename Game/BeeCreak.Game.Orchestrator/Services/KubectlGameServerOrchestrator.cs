using System.Globalization;
using System.Text.Json;
using BeeCreak.Game.Orchestrator.Models;
using BeeCreak.Game.Orchestrator.Options;
using BeeCreak.Game.Orchestrator.Utilities;
using Microsoft.Extensions.Options;

namespace BeeCreak.Game.Orchestrator.Services;

public sealed class KubectlGameServerOrchestrator : IGameServerOrchestrator
{
    private readonly KubectlInvoker _kubectl;
    private readonly OrchestratorOptions _options;

    public KubectlGameServerOrchestrator(KubectlInvoker kubectl, IOptions<OrchestratorOptions> options)
    {
        _kubectl = kubectl;
        _options = options.Value;

        if (string.IsNullOrWhiteSpace(_options.Namespace))
        {
            throw new InvalidOperationException("Orchestrator namespace must be configured.");
        }
    }

    public async Task<IReadOnlyList<GameServerDto>> ListAsync(CancellationToken cancellationToken)
    {
        var pods = await ListPodsAsync(cancellationToken);
        var services = await ListServicesAsync(cancellationToken);
        var nodeAddress = await TryGetNodeAddressAsync(cancellationToken);

        var servers = new List<GameServerDto>();
        foreach (var pod in pods)
        {
            var service = services.FirstOrDefault(s => string.Equals(s.ServerId, pod.ServerId, StringComparison.Ordinal));
            servers.Add(ToDto(pod, service, nodeAddress));
        }

        return servers;
    }

    public async Task<GameServerDto?> GetAsync(string id, CancellationToken cancellationToken)
    {
        var pods = await ListPodsAsync(cancellationToken, id);
        var pod = pods.FirstOrDefault();
        if (pod is null)
        {
            return null;
        }

        var services = await ListServicesAsync(cancellationToken, id);
        var service = services.FirstOrDefault();
        var nodeAddress = await TryGetNodeAddressAsync(cancellationToken);
        return ToDto(pod, service, nodeAddress);
    }

    public async Task<GameServerDto> CreateAsync(CreateServerRequest request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid().ToString("n", CultureInfo.InvariantCulture);
        var name = BuildResourceName(id);
        var kubeNamespace = _options.Namespace;
        var image = string.IsNullOrWhiteSpace(request.Image) ? _options.ServerImage : request.Image.Trim();
        var port = request.ContainerPort ?? _options.ContainerPort;
        var exposeNodePort = request.ExposeNodePort ?? _options.ExposeNodePort;

        var labels = BuildLabels(id);
        var annotations = BuildAnnotations(request.Metadata, image, request.Args);

        var args = BuildArgs(id, port, request.Args);

        var podYaml = KubernetesYaml.BuildPodYaml(
            name,
            kubeNamespace,
            labels,
            annotations,
            image,
            port,
            _options.HealthPort,
            args,
            _options.HeartbeatPath,
            _options.HeartbeatInitialDelaySeconds,
            _options.HeartbeatPeriodSeconds,
            _options.RestartPolicy);

        var serviceYaml = KubernetesYaml.BuildServiceYaml(
            name,
            kubeNamespace,
            labels,
            new Dictionary<string, string> { { _options.ServerIdLabelKey, id } },
            port,
            exposeNodePort);

        await _kubectl.RunYamlAsync("apply -f -", podYaml, cancellationToken);
        await _kubectl.RunYamlAsync("apply -f -", serviceYaml, cancellationToken);

        var service = await GetServiceAsync(id, cancellationToken);
        var nodeAddress = await TryGetNodeAddressAsync(cancellationToken);
        var podSnapshot = new PodSnapshot
        {
            ServerId = id,
            Name = name,
            Status = GameServerStatus.Provisioning,
            CreatedAt = DateTimeOffset.UtcNow,
            Image = image,
            ContainerPort = port,
            Metadata = request.Metadata
        };

        return ToDto(podSnapshot, service, nodeAddress);
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var labelSelector = BuildServerLabelSelector(id);
        var kubeNamespace = _options.Namespace;
        await _kubectl.RunAsync($"delete pod -n {Escape(kubeNamespace)} -l {Escape(labelSelector)} --ignore-not-found", cancellationToken);
        await _kubectl.RunAsync($"delete service -n {Escape(kubeNamespace)} -l {Escape(labelSelector)} --ignore-not-found", cancellationToken);
        return true;
    }

    private Dictionary<string, string> BuildLabels(string serverId)
    {
        return new Dictionary<string, string>
        {
            [_options.OrchestratorLabelKey] = _options.OrchestratorLabelValue,
            [_options.ServerIdLabelKey] = serverId,
            [_options.AppLabelKey] = _options.AppLabelValue
        };
    }

    private Dictionary<string, string> BuildAnnotations(Dictionary<string, string>? metadata, string image, List<string>? args)
    {
        var annotations = new Dictionary<string, string>
        {
            [_options.ImageAnnotationKey] = image,
            [_options.CreatedAtAnnotationKey] = DateTimeOffset.UtcNow.ToString("O", CultureInfo.InvariantCulture)
        };

        if (metadata is { Count: > 0 })
        {
            annotations[_options.MetadataAnnotationKey] = KubernetesYaml.ToJson(metadata);
        }

        if (args is { Count: > 0 })
        {
            annotations[_options.ArgsAnnotationKey] = KubernetesYaml.ToJson(args);
        }

        return annotations;
    }

    private List<string> BuildArgs(string serverId, int port, List<string>? requestArgs)
    {
        var args = new List<string>();
        foreach (var arg in _options.DefaultArgs)
        {
            args.Add(arg.Replace("{serverId}", serverId, StringComparison.Ordinal)
                .Replace("{port}", port.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal)
                .Replace("{healthPort}", _options.HealthPort.ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal)
                .Replace("{heartbeatPath}", _options.HeartbeatPath, StringComparison.Ordinal));
        }

        if (requestArgs is { Count: > 0 })
        {
            args.AddRange(requestArgs);
        }

        return args;
    }

    private string BuildResourceName(string serverId)
    {
        var baseName = $"gs-{serverId}";
        return baseName.Length <= 63 ? baseName : baseName[..63];
    }

    private string BuildServerLabelSelector(string serverId)
    {
        return $"{_options.ServerIdLabelKey}={serverId},{_options.OrchestratorLabelKey}={_options.OrchestratorLabelValue}";
    }

    private async Task<List<PodSnapshot>> ListPodsAsync(CancellationToken cancellationToken, string? serverId = null)
    {
        var kubeNamespace = _options.Namespace;
        var selector = serverId is null ? _options.LabelSelector : BuildServerLabelSelector(serverId);
        var json = await _kubectl.RunAsync($"get pods -n {Escape(kubeNamespace)} -l {Escape(selector)} -o json", cancellationToken);
        return ParsePods(
            json,
            _options.ServerIdLabelKey,
            _options.ImageAnnotationKey,
            _options.CreatedAtAnnotationKey,
            _options.MetadataAnnotationKey);
    }

    private async Task<List<ServiceSnapshot>> ListServicesAsync(CancellationToken cancellationToken, string? serverId = null)
    {
        var kubeNamespace = _options.Namespace;
        var selector = serverId is null ? _options.LabelSelector : BuildServerLabelSelector(serverId);
        var json = await _kubectl.RunAsync($"get services -n {Escape(kubeNamespace)} -l {Escape(selector)} -o json", cancellationToken);
        return ParseServices(json, _options.ServerIdLabelKey);
    }

    private async Task<ServiceSnapshot?> GetServiceAsync(string serverId, CancellationToken cancellationToken)
    {
        var services = await ListServicesAsync(cancellationToken, serverId);
        return services.FirstOrDefault();
    }

    private async Task<string?> TryGetNodeAddressAsync(CancellationToken cancellationToken)
    {
        try
        {
            var json = await _kubectl.RunAsync("get nodes -o json", cancellationToken);
            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("items", out var items))
            {
                return null;
            }

            foreach (var item in items.EnumerateArray())
            {
                if (!item.TryGetProperty("status", out var status))
                {
                    continue;
                }

                if (!status.TryGetProperty("addresses", out var addresses))
                {
                    continue;
                }

                foreach (var address in addresses.EnumerateArray())
                {
                    var type = address.GetProperty("type").GetString();
                    var value = address.GetProperty("address").GetString();
                    if (string.Equals(type, _options.PreferredNodeAddressType, StringComparison.OrdinalIgnoreCase))
                    {
                        return value;
                    }
                }
            }
        }
        catch
        {
            return null;
        }

        return null;
    }

    private static List<PodSnapshot> ParsePods(
        string json,
        string serverIdLabelKey,
        string imageAnnotationKey,
        string createdAtAnnotationKey,
        string metadataAnnotationKey)
    {
        var pods = new List<PodSnapshot>();
        using var doc = JsonDocument.Parse(json);
        if (!doc.RootElement.TryGetProperty("items", out var items))
        {
            return pods;
        }

        foreach (var item in items.EnumerateArray())
        {
            var metadata = item.GetProperty("metadata");
            var labels = GetStringMap(metadata, "labels");
            if (!labels.TryGetValue(serverIdLabelKey, out var serverId))
            {
                continue;
            }

            var annotations = GetStringMap(metadata, "annotations");
            var status = item.TryGetProperty("status", out var statusElement)
                ? MapStatus(statusElement.GetProperty("phase").GetString())
                : GameServerStatus.Unknown;

            var containerPort = GetContainerPort(item);
            var image = GetImage(item) ?? annotations.GetValueOrDefault(imageAnnotationKey);
            var createdAt = GetDateTimeOffset(annotations.GetValueOrDefault(createdAtAnnotationKey))
                ?? GetDateTimeOffset(metadata.GetProperty("creationTimestamp").GetString());

            pods.Add(new PodSnapshot
            {
                ServerId = serverId,
                Name = metadata.GetProperty("name").GetString() ?? string.Empty,
                Status = status,
                CreatedAt = createdAt ?? DateTimeOffset.UtcNow,
                Image = image,
                ContainerPort = containerPort,
                Metadata = ParseMetadata(annotations.GetValueOrDefault(metadataAnnotationKey))
            });
        }

        return pods;
    }

    private static List<ServiceSnapshot> ParseServices(string json, string serverIdLabelKey)
    {
        var services = new List<ServiceSnapshot>();
        using var doc = JsonDocument.Parse(json);
        if (!doc.RootElement.TryGetProperty("items", out var items))
        {
            return services;
        }

        foreach (var item in items.EnumerateArray())
        {
            var metadata = item.GetProperty("metadata");
            var labels = GetStringMap(metadata, "labels");
            if (!labels.TryGetValue(serverIdLabelKey, out var serverId))
            {
                continue;
            }

            var spec = item.GetProperty("spec");
            var ports = spec.GetProperty("ports");
            int? nodePort = null;
            int? port = null;
            if (ports.GetArrayLength() > 0)
            {
                var portElement = ports[0];
                if (portElement.TryGetProperty("nodePort", out var nodePortElement))
                {
                    nodePort = nodePortElement.GetInt32();
                }
                if (portElement.TryGetProperty("port", out var portValue))
                {
                    port = portValue.GetInt32();
                }
            }

            services.Add(new ServiceSnapshot
            {
                ServerId = serverId,
                Name = metadata.GetProperty("name").GetString() ?? string.Empty,
                NodePort = nodePort,
                Port = port
            });
        }

        return services;
    }

    private GameServerDto ToDto(PodSnapshot pod, ServiceSnapshot? service, string? nodeAddress)
    {
        var nodePort = service?.NodePort;
        var endpoint = nodePort.HasValue && !string.IsNullOrWhiteSpace(nodeAddress)
            ? $"{nodeAddress}:{nodePort.Value}"
            : null;

        return new GameServerDto
        {
            Id = pod.ServerId,
            Status = pod.Status,
            CreatedAt = pod.CreatedAt,
            Image = pod.Image,
            ContainerPort = pod.ContainerPort,
            Endpoint = endpoint,
            NodePort = nodePort,
            PodName = pod.Name,
            ServiceName = service?.Name,
            Metadata = pod.Metadata
        };
    }

    private static Dictionary<string, string> GetStringMap(JsonElement element, string propertyName)
    {
        var map = new Dictionary<string, string>(StringComparer.Ordinal);
        if (!element.TryGetProperty(propertyName, out var mapElement))
        {
            return map;
        }

        foreach (var property in mapElement.EnumerateObject())
        {
            if (property.Value.ValueKind == JsonValueKind.String)
            {
                map[property.Name] = property.Value.GetString() ?? string.Empty;
            }
        }

        return map;
    }

    private static int? GetContainerPort(JsonElement pod)
    {
        if (!pod.TryGetProperty("spec", out var spec))
        {
            return null;
        }

        if (!spec.TryGetProperty("containers", out var containers) || containers.GetArrayLength() == 0)
        {
            return null;
        }

        var container = containers[0];
        if (!container.TryGetProperty("ports", out var ports) || ports.GetArrayLength() == 0)
        {
            return null;
        }

        return ports[0].GetProperty("containerPort").GetInt32();
    }

    private static string? GetImage(JsonElement pod)
    {
        if (!pod.TryGetProperty("spec", out var spec))
        {
            return null;
        }

        if (!spec.TryGetProperty("containers", out var containers) || containers.GetArrayLength() == 0)
        {
            return null;
        }

        return containers[0].GetProperty("image").GetString();
    }

    private static GameServerStatus MapStatus(string? phase)
    {
        return phase switch
        {
            "Pending" => GameServerStatus.Provisioning,
            "Running" => GameServerStatus.Running,
            "Failed" => GameServerStatus.Failed,
            "Succeeded" => GameServerStatus.Succeeded,
            _ => GameServerStatus.Unknown
        };
    }

    private static Dictionary<string, string>? ParseMetadata(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        catch
        {
            return null;
        }
    }

    private static DateTimeOffset? GetDateTimeOffset(string? value)
    {
        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var result))
        {
            return result;
        }

        return null;
    }

    private static string Escape(string value)
    {
        if (value.Contains(' '))
        {
            return '"' + value.Replace("\"", "\\\"") + '"';
        }

        return value;
    }

    private sealed class PodSnapshot
    {
        public string ServerId { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public GameServerStatus Status { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public string? Image { get; init; }
        public int? ContainerPort { get; init; }
        public Dictionary<string, string>? Metadata { get; init; }
    }

    private sealed class ServiceSnapshot
    {
        public string ServerId { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public int? NodePort { get; init; }
        public int? Port { get; init; }
    }
}
