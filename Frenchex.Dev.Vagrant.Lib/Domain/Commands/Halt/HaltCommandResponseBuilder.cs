using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;

public class HaltCommandResponseBuilder : RootResponseBuilder, IHaltCommandResponseBuilder
{
    public IHaltCommandResponse Build()
    {
        if (null == Process || null == ProcessExecutionResult)
            throw new InvalidOperationException("process or execution result is null");

        return new HaltCommandResponse(Process, ProcessExecutionResult);
    }
}