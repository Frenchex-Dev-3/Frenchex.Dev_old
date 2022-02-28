namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Halt;

public class HaltCommandIntegrationPayload : CommandIntegrationPayload
{
    public string[]? Names { get; set; }
    public bool Force { get; set; }
    public int HaltTimeoutMs { get; set; }
}