using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;

public class StatusCommandResponseBuilder : RootResponseBuilder, IStatusCommandResponseBuilder
{
    public IStatusCommandResponse Build()
    {
        if (null == _process || null == _processExecutionResult)
            throw new InvalidOperationException("process or processexecutionresult is null");

        return new StatusCommandResponse(
            _process,
            _processExecutionResult
        );
    }
}