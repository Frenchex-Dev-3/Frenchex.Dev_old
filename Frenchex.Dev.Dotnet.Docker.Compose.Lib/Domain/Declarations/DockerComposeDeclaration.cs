namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;

public class DockerComposeDeclaration
{
    public string? Version { get; set; }
    
    public Dictionary<string, DockerComposeServiceDeclaration>? Services { get; set; }

    public Dictionary<string, DockerComposeNetworkDeclaration>? Networks { get; set; }
    
    public Dictionary<string, DockerComposeVolumeDeclaration>? Volumes { get; set; }

}