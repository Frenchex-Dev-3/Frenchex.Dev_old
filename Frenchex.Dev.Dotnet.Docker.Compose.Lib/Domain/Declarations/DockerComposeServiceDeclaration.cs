namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;

public class DockerComposeServiceDeclaration
{
    public DockerComposeServiceBuildDeclaration? Build { get; set; }
    
    public string? Image { get; set; }
    
    public string[]? Ports { get; set; }
    
    public string[]? Networks { get; set; }
    
    public DockerComposeServiceDeployDeclaration? Deploy { get; set; }
    
    public string[]? Volumes { get; set; }
    
    public string[]? DependsOn { get; set; }
    
    public string? StopGracePeriod { get; set; }
}