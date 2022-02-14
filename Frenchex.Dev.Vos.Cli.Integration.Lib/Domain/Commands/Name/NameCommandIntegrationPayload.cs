namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Name
{
    public class NameCommandIntegrationPayload
    {
        public string[]? Names { get; set; }
        public string? WorkingDirectory { get; set; }
        public int TimeoutMs { get; set; }

    }
}
