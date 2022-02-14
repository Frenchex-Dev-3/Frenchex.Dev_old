namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Destroy
{
    public class DestroyCommandIntegrationPayload
    {
        public string[]? NameOrId { get; set; }
        public bool Force { get; set; }
        public bool Graceful { get; set; }
        public bool Parallel { get; set; }
        public int TimeoutMs { get; set; }
        public string? WorkingDirectory { get; set; }
    }
}
