using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init
{
    public class InitCommandResponse : RootResponse, IInitCommandResponse
    {
        public InitCommandResponse(
            IProcess process,
            ProcessExecutionResult processExecutionResult
        ) : base(process, processExecutionResult)
        {
        }
    }
}
