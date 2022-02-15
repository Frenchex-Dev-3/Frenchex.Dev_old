using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;

public class SshCommandResponseBuilder : RootResponseBuilder, ISshCommandResponseBuilder
{
    public ISshCommandResponse Build()
    {
        if (null == Process || null == ProcessExecutionResult)
            throw new InvalidOperationException("process or processexecutionresult is null");

        return new SshCommandResponse(Process, ProcessExecutionResult);
    }
}