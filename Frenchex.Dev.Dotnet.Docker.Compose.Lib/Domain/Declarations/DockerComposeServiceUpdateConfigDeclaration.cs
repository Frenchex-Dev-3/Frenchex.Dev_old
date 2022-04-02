namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;

public class DockerComposeServiceUpdateConfigDeclaration
{
    public int? Parallelism { get; set; }
    
    public string? Delay { get; set; }
}