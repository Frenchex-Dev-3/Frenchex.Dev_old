namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Up;

public class UpCommandIntegrationPayload : CommandIntegrationPayload
{
    public string[]? Names { get; set; }
    public bool Provision { get; set; }
    public string[]? ProvisionWith { get; set; }
    public bool DestroyOnError { get; set; }
    public bool Parallel { get; set; }
    public int ParallelWorkers { get; set; }
    public int ParallelWait { get; set; }
    public string? Provider { get; set; }
    public bool InstallProvider { get; set; }
}