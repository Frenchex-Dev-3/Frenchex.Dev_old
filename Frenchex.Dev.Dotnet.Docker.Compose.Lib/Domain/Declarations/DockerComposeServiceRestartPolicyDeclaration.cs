namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;

public class DockerComposeServiceRestartPolicyDeclaration
{
    public string? Condition { get; set; }
    
    public string? Delay { get; set; }
    
    public int? MaxAttempts { get; set; }
    
    public string? Window { get; set; }
}