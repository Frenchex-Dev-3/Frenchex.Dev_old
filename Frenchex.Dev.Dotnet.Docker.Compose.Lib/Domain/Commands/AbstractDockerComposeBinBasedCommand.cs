using System.Text;
using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Microsoft.Extensions.Configuration;

namespace Frenchex.Dev.Dotnet.Docker.Compose.Lib.Domain.Commands;

public class AbstractDockerComposeBinBasedCommand
{
    protected IProcessBuilder ProcessBuilder;
    protected IFilesystem Filesystem;
    
    protected AbstractDockerComposeBinBasedCommand(
        IProcessBuilder processBuilder,
        IFilesystem fileSystem,
        IConfiguration configuration
    )
    {
        ProcessBuilder = processBuilder;
        Filesystem = fileSystem;
    }
    
    private string GetBinary()
    {
        return "docker";
    }
    
    protected ProcessExecutionResult BuildAndStartProcess(
        string workingDirectory,
        int timeoutMs,
        string arguments
    )
    {
        var process = Build(new ProcessBuildingParameters(
            GetBinary(),
            arguments,
            workingDirectory,
            timeoutMs,
            false,
            true,
            true,
            true,
            true
        ));

        var processExecutionResult = process.Start();

        return processExecutionResult;
    }

    protected IProcess Build(ProcessBuildingParameters buildParameters)
    {
        return ProcessBuilder.Build(buildParameters);
    }

    protected static string BuildRootVagrantOptions(BaseDockerComposeBinBasedRequest request)
    {
        return new StringBuilder()
                .ToString()
            ;
    }
}