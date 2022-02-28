namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public class CommandIntegrationPayload
{
    public string? VagrantBinPath { get; set; }
    public string? WorkingDirectory { get; set; }
    public int TimeoutMs { get; set; }
}