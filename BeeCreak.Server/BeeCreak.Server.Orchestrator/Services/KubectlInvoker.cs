using System.Diagnostics;
using BeeCreak.Orchestrator.Options;
using Microsoft.Extensions.Options;

namespace BeeCreak.Orchestrator.Services;

public sealed class KubectlInvoker
{
    private readonly OrchestratorOptions _options;

    public KubectlInvoker(IOptions<OrchestratorOptions> options)
    {
        _options = options.Value;
    }

    public async Task<string> RunAsync(string arguments, CancellationToken cancellationToken)
    {
        var psi = new ProcessStartInfo("kubectl", BuildArguments(arguments))
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using var process = new Process { StartInfo = psi };
        try
        {
            process.Start();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to start kubectl. Ensure kubectl is installed and available on PATH.", ex);
        }

        var stdoutTask = process.StandardOutput.ReadToEndAsync(cancellationToken);
        var stderrTask = process.StandardError.ReadToEndAsync(cancellationToken);

        await process.WaitForExitAsync(cancellationToken);

        var stdout = await stdoutTask;
        var stderr = await stderrTask;

        if (process.ExitCode != 0)
        {
            var message = string.IsNullOrWhiteSpace(stderr)
                ? "kubectl exited with a non-zero status."
                : stderr.Trim();
            throw new InvalidOperationException(message);
        }

        return stdout;
    }

    public async Task<string> RunYamlAsync(string arguments, string yaml, CancellationToken cancellationToken)
    {
        var psi = new ProcessStartInfo("kubectl", BuildArguments(arguments))
        {
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using var process = new Process { StartInfo = psi };
        try
        {
            process.Start();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to start kubectl. Ensure kubectl is installed and available on PATH.", ex);
        }

        await process.StandardInput.WriteAsync(yaml);
        process.StandardInput.Close();

        var stdoutTask = process.StandardOutput.ReadToEndAsync(cancellationToken);
        var stderrTask = process.StandardError.ReadToEndAsync(cancellationToken);

        await process.WaitForExitAsync(cancellationToken);

        var stdout = await stdoutTask;
        var stderr = await stderrTask;

        if (process.ExitCode != 0)
        {
            var message = string.IsNullOrWhiteSpace(stderr)
                ? "kubectl exited with a non-zero status."
                : stderr.Trim();
            throw new InvalidOperationException(message);
        }

        return stdout;
    }

    private string BuildArguments(string arguments)
    {
        var baseArgs = new List<string>();
        if (!string.IsNullOrWhiteSpace(_options.KubeConfigPath))
        {
            baseArgs.Add("--kubeconfig");
            baseArgs.Add(EscapeArgument(_options.KubeConfigPath));
        }

        if (!string.IsNullOrWhiteSpace(_options.KubeContext))
        {
            baseArgs.Add("--context");
            baseArgs.Add(EscapeArgument(_options.KubeContext));
        }

        if (baseArgs.Count == 0)
        {
            return arguments;
        }

        return string.Join(' ', baseArgs) + " " + arguments;
    }

    private static string EscapeArgument(string value)
    {
        if (value.Contains(' '))
        {
            return '"' + value.Replace("\"", "\\\"") + '"';
        }

        return value;
    }
}
