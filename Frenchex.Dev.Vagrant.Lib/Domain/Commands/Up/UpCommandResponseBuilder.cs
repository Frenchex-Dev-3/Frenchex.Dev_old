using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up;

public class UpCommandResponseBuilder : RootResponseBuilder, IUpCommandResponseBuilder
{
    public IUpCommandResponse Build()
    {
        if (null == Process || null == ProcessExecutionResult)
            throw new InvalidOperationException("missing process or execution result");

        return new UpCommandResponse(Process, ProcessExecutionResult);
    }
}