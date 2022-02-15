using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;

public class DestroyCommandResponseBuilder : RootResponseBuilder, IDestroyCommandResponseBuilder
{
    public IDestroyCommandResponse Build()
    {
        if (null == Process || null == ProcessExecutionResult)
            throw new InvalidOperationException("missing process or execution result");

        return new DestroyCommandResponse(Process, ProcessExecutionResult);
    }
}