using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandResponseBuilder : RootResponseBuilder, ISshConfigCommandResponseBuilder
{
    public ISshConfigCommandResponse Build()
    {
        if (null == Process) throw new InvalidOperationException("process is null");

        if (null == ProcessExecutionResult) throw new InvalidOperationException("processExecutionResult is null");

        return new SshConfigCommandResponse(Process, ProcessExecutionResult);
    }
}