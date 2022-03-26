namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain;

public class DockerComposeDeclaration
{
    public string? Version { get; set; }
    public Dictionary<string, DockerComposeServiceClaration>? Services { get; set; }
    
    
}

public class DockerComposeServiceClaration
{
    public BuildDeclaration? Build { get; set; }
    public string Image { get; set; }
}

public class BuildDeclaration
{
    public string? Context { get; set; }
    public string? Dockerfile { get; set; }
    public Dictionary<string, string> Args { get; set; }
    
}   