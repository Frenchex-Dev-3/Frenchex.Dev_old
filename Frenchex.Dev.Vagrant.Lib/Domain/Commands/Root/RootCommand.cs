using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using System.Text;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root
{
    abstract public class RootCommand : IRootCommand
    {
        protected IProcessBuilder _processBuilder;
        protected IFilesystem _filesystem;

        public RootCommand(
            IProcessBuilder processBuilder,
            IFilesystem fileSystem
        )
        {
            _processBuilder = processBuilder;
            _filesystem = fileSystem;
        }

        protected static string GetBinary()
        {
            return "vagrant";
        }

        protected ProcessExecutionResult BuildAndStartProcess(
           IRootCommandRequest request,
           IRootCommandResponseBuilder responseBuilder,
           string arguments
       )
        {
            var process = Build(new ProcessBuildingParameters(
                    command: GetBinary(),
                    arguments: arguments,
                    workingDirectory: request.Base.WorkingDirectory,
                    timeoutMs: request.Base.TimeoutInMiliSeconds,
                    useShellExecute: false,
                    redirectStandardInput: true,
                    redirectStandardOuput: true,
                    redirectStandardError: true,
                    createNoWindow: true
                ));

            var processExecutionResult = process.Start();

            responseBuilder
                .SetProcess(process)
                .SetProcessExecutionResult(processExecutionResult)
                ;

            return processExecutionResult;
        }

        protected IProcess Build(ProcessBuildingParameters buildParameters)
        {
            return _processBuilder.Build(buildParameters);
        }

        protected static string BuildRootVagrantOptions(IBaseCommandRequest request)
        {
            return new StringBuilder()
                .Append(request.Color ? " --color" : " --no-color")
                .Append(request.MachineReadable ? " --machine-readable" : "")
                .Append(request.Version ? " --version" : "")
                .Append(request.Debug ? " --debug" : "")
                .Append(request.Timestamp ? " --timestamp" : "")
                .Append(request.DebugTimestamp ? " --debug-timestamp" : "")
                .Append(request.Tty ? " --tty" : " --no-tty")
                .Append(request.Help ? " --help" : "")
                .ToString()
                ;
        }
    }
}
