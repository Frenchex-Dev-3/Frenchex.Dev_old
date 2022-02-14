namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Init
{
    public class InitCommandIntegrationPayload
    {
#pragma warning disable CS8618
        public string Naming { get; set; }
#pragma warning restore CS8618
        public int Zeroes { get; set; }
        public int TimeoutMs { get; set; }
#pragma warning disable CS8618
        public string? WorkingDirectory { get; set; }
#pragma warning restore CS8618
    }
}
