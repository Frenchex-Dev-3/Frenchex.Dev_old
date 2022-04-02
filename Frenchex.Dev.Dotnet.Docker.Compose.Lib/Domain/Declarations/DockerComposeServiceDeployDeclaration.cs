namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;

public class DockerComposeServiceDeployDeclaration
{
    public int? Replicas { get; set; }
    
    public DockerComposeServiceUpdateConfigDeclaration? UpdateConfig { get; set; }
    
    public DockerComposeServiceRestartPolicyDeclaration? RestartPolicy { get; set; }
    
    public DockerComposeServicePlacementDeclaration? Placement { get; set; }
    
    public string? Mode { get; set; }
    
    public string[]? Labels { get; set; }
}