using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy
{

    public class DestroyCommandResponse : RootResponse, IDestroyCommandResponse
    {
        public DestroyCommandResponse(
            IProcess process,
            ProcessExecutionResult processExecutionResult
        ) : base(process, processExecutionResult)
        {
        }
    }

}
