using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandResponse : RootResponse, ISshConfigCommandResponse
{
    public SshConfigCommandResponse(
        IProcess process,
        ProcessExecutionResult processExecutionResult
    ) : base(process, processExecutionResult)
    {
    }
}