namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Up
{
    public class UpCommandIntegrationPayload
    {
        public string[]? Names { get; set; }
        public bool Provision { get; set; }
        public string[]? ProvisionWith { get; set; }
        public bool DestroyOnError { get; set; }
        public bool Parallel { get; set; }
        public string? Provider { get; set; }
        public bool InstallProvider { get; set; }
        public int TimeoutMs { get; set; }
        public string? WorkingDirectory { get; set; }
    }
}
