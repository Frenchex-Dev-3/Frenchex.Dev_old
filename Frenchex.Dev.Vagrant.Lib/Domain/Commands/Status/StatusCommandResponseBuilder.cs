using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;

public class StatusCommandResponseBuilder : RootResponseBuilder, IStatusCommandResponseBuilder
{
    public IStatusCommandResponse Build()
    {
        if (null == Process || null == ProcessExecutionResult)
            throw new InvalidOperationException("process or processexecutionresult is null");

        return new StatusCommandResponse(
            Process,
            ProcessExecutionResult
        );
    }
}