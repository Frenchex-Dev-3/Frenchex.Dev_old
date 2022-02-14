namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Status;

public class StatusCommandIntegrationPayload
{
    public string[]? Names { get; set; }
    public string? WorkingDirectory { get; set; }
    public int TimeoutMs { get; set; }
}