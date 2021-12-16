using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh
{
    public class SshCommandResponse : RootResponse, ISshCommandResponse
    {
        public SshCommandResponse(
            IProcess process,
            ProcessExecutionResult processExecutionResult
        ) : base(process, processExecutionResult)
        {
        }
    }
}
