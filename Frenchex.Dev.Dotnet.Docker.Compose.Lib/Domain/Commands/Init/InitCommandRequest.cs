using Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Declarations;

namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands.Init;

public class InitCommandRequest : AbstractCommandRequest
{
    public DockerComposeDeclaration Declaration { get; set; }
}