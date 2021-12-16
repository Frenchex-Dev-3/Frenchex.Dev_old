using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up
{
    public class UpCommandResponse : RootResponse, IUpCommandResponse
    {
        public UpCommandResponse(
            IProcess process,
            ProcessExecutionResult processExecutionResult
        ) : base(process, processExecutionResult)
        {
        }
    }
}
