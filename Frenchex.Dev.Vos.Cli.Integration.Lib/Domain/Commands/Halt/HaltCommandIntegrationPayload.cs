namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Halt;

public class HaltCommandIntegrationPayload
{
    public int TimeoutMs { get; set; }
    public string[]? Names { get; set; }
    public string? WorkingDirectory { get; set; }
    public bool Force { get; set; }
    public int HaltTimeoutMs { get; set; }
}