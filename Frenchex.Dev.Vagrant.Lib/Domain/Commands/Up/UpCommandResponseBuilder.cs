using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up;

public class UpCommandResponseBuilder : RootResponseBuilder, IUpCommandResponseBuilder
{
    public IUpCommandResponse Build()
    {
        if (null == _process || null == _processExecutionResult)
            throw new InvalidOperationException("missing process or execution result");

        return new UpCommandResponse(_process, _processExecutionResult);
    }
}