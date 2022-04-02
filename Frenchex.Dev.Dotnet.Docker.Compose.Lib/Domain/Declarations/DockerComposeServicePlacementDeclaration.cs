namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;

public class DockerComposeServicePlacementDeclaration
{
    public int? MaxReplicasPerNode { get; set; }

    public string[]? Constraints { get; set; }
}