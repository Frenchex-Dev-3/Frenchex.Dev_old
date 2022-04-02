namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands;

public class AbstractCommandRequest
{
    public string? WorkingDirectory { get; set; }
    public int TimeoutMs { get; set; }
}