using System.Text;
using System.Text.Json;

namespace BeeCreak.Game.Orchestrator.Utilities;

public static class KubernetesYaml
{
    public static string BuildPodYaml(
        string name,
        string kubeNamespace,
        IReadOnlyDictionary<string, string> labels,
        IReadOnlyDictionary<string, string> annotations,
        string image,
        int containerPort,
        int healthPort,
        IReadOnlyList<string> args,
        string heartbeatPath,
        int initialDelaySeconds,
        int periodSeconds,
        string restartPolicy)
    {
        var builder = new StringBuilder();
        builder.AppendLine("apiVersion: v1");
        builder.AppendLine("kind: Pod");
        builder.AppendLine("metadata:");
        builder.AppendLine($"  name: {Escape(name)}");
        builder.AppendLine($"  namespace: {Escape(kubeNamespace)}");
        if (labels.Count > 0)
        {
            builder.AppendLine("  labels:");
            foreach (var (key, value) in labels)
            {
                builder.AppendLine($"    {EscapeKey(key)}: {Escape(value)}");
            }
        }
        if (annotations.Count > 0)
        {
            builder.AppendLine("  annotations:");
            foreach (var (key, value) in annotations)
            {
                builder.AppendLine($"    {EscapeKey(key)}: {Escape(value)}");
            }
        }
        builder.AppendLine("spec:");
        builder.AppendLine($"  restartPolicy: {Escape(restartPolicy)}");
        builder.AppendLine("  containers:");
        builder.AppendLine("  - name: gameserver");
        builder.AppendLine($"    image: {Escape(image)}");
        if (args.Count > 0)
        {
            builder.AppendLine("    args:");
            foreach (var arg in args)
            {
                builder.AppendLine($"    - {Escape(arg)}");
            }
        }
        builder.AppendLine("    ports:");
        builder.AppendLine($"    - containerPort: {containerPort}");
        builder.AppendLine("      protocol: UDP");
        builder.AppendLine($"    - containerPort: {healthPort}");
        builder.AppendLine("      protocol: TCP");
        builder.AppendLine("    livenessProbe:");
        builder.AppendLine("      httpGet:");
        builder.AppendLine($"        path: {Escape(heartbeatPath)}");
        builder.AppendLine($"        port: {healthPort}");
        builder.AppendLine($"      initialDelaySeconds: {initialDelaySeconds}");
        builder.AppendLine($"      periodSeconds: {periodSeconds}");
        builder.AppendLine("    readinessProbe:");
        builder.AppendLine("      httpGet:");
        builder.AppendLine($"        path: {Escape(heartbeatPath)}");
        builder.AppendLine($"        port: {healthPort}");
        builder.AppendLine($"      initialDelaySeconds: {initialDelaySeconds}");
        builder.AppendLine($"      periodSeconds: {periodSeconds}");
        return builder.ToString();
    }

    public static string BuildServiceYaml(
        string name,
        string kubeNamespace,
        IReadOnlyDictionary<string, string> labels,
        IReadOnlyDictionary<string, string> selector,
        int port,
        bool useNodePort)
    {
        var builder = new StringBuilder();
        builder.AppendLine("apiVersion: v1");
        builder.AppendLine("kind: Service");
        builder.AppendLine("metadata:");
        builder.AppendLine($"  name: {Escape(name)}");
        builder.AppendLine($"  namespace: {Escape(kubeNamespace)}");
        if (labels.Count > 0)
        {
            builder.AppendLine("  labels:");
            foreach (var (key, value) in labels)
            {
                builder.AppendLine($"    {EscapeKey(key)}: {Escape(value)}");
            }
        }
        builder.AppendLine("spec:");
        builder.AppendLine($"  type: {(useNodePort ? "NodePort" : "ClusterIP")}");
        builder.AppendLine("  selector:");
        foreach (var (key, value) in selector)
        {
            builder.AppendLine($"    {EscapeKey(key)}: {Escape(value)}");
        }
        builder.AppendLine("  ports:");
        builder.AppendLine("  - name: game");
        builder.AppendLine($"    port: {port}");
        builder.AppendLine($"    targetPort: {port}");
        builder.AppendLine("    protocol: UDP");
        return builder.ToString();
    }

    public static string ToJson<T>(T value)
        => JsonSerializer.Serialize(value, new JsonSerializerOptions { WriteIndented = false });

    private static string Escape(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "''";
        }

        var needsQuotes = value.Any(ch => char.IsWhiteSpace(ch) || ch == ':' || ch == '#');
        if (!needsQuotes)
        {
            return value;
        }

        return "'" + value.Replace("'", "''") + "'";
    }

    private static string EscapeKey(string value)
    {
        return Escape(value);
    }
}
