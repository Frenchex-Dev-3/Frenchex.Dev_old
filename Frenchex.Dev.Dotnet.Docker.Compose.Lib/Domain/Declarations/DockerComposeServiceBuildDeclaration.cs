namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;

public class DockerComposeServiceBuildDeclaration
{
    public string? Context { get; set; }
    
    public string? Dockerfile { get; set; }
    
    public Dictionary<string, string>? Args { get; set; }
}