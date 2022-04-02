using System.Text;
using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Microsoft.Extensions.Configuration;

namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands.Ps;

public interface IPsCommand : ICommand<PsCommandRequest, PsCommandResponse>
{
    
}

public class PsCommand : AbstractDockerComposeBinBasedCommand, IPsCommand
{
    public PsCommand(
        IProcessBuilder processBuilder,
        IFilesystem fileSystem,
        IConfiguration configuration
    ) : base(processBuilder, fileSystem, configuration)
    {
    }

    public PsCommandResponse Execute(PsCommandRequest request)
    {
        var task = BuildAndStartProcess(
            request.WorkingDirectory,
            request.TimeoutMs,
            BuildArguments(request)
        );

        return new PsCommandResponse(task);
    }

    private static string BuildArguments(PsCommandRequest request)
    {
        return GetCliCommandName() + " " + BuildDockerComposeOptions(request) + " " +
               BuildDockerComposeArguments(request);
    }

    private static string GetCliCommandName()
    {
        return "compose ps";
    }

    private static string BuildDockerComposeOptions(PsCommandRequest request)
    {
        return new StringBuilder()
                .ToString()
            ;
    }

    private static string BuildDockerComposeArguments(PsCommandRequest request)
    {
        return new StringBuilder()
                .ToString()
            ;
    }

    protected static string BuildArguments(string command, PsCommandRequest request)
    {
        return
            $"{command} " +
            $"{BuildDockerComposeOptions(request)} " +
            $"{BuildDockerComposeArguments(request)}"
            ;
    }
}