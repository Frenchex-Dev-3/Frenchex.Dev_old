using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public abstract class RootResponseBuilder : IRootCommandResponseBuilder
{
    protected IProcess? Process;
    protected ProcessExecutionResult? ProcessExecutionResult;

    public IRootCommandResponseBuilder SetProcess(IProcess process)
    {
        Process = process;
        return this;
    }

    public IRootCommandResponseBuilder SetProcessExecutionResult(ProcessExecutionResult processExecutionResult)
    {
        ProcessExecutionResult = processExecutionResult;
        return this;
    }
}