using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status
{
    public class StatusCommandResponse : RootResponse, IStatusCommandResponse
    {
        public StatusCommandResponse(
            IProcess process,
            ProcessExecutionResult processExecutionResult
        ) : base(process, processExecutionResult)
        {

        }

    }
}
