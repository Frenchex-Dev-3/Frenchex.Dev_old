using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;

public class InitCommandResponseBuilder : RootResponseBuilder, IInitCommandResponseBuilder
{
    public IInitCommandResponse Build()
    {
        if (null == Process) throw new InvalidOperationException("process is null");

        if (null == ProcessExecutionResult) throw new InvalidOperationException("processExecutionResult is null");

        return new InitCommandResponse(Process, ProcessExecutionResult);
    }
}