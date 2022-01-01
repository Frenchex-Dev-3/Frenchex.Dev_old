using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;

public class InitCommandResponseBuilder : RootResponseBuilder, IInitCommandResponseBuilder
{
    public IInitCommandResponse Build()
    {
        if (null == _process) throw new InvalidOperationException("process is null");

        if (null == _processExecutionResult) throw new InvalidOperationException("processExecutionResult is null");

        return new InitCommandResponse(_process, _processExecutionResult);
    }
}