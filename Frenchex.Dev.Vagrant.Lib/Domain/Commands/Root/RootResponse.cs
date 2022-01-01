using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public class RootResponse : IRootCommandResponse
{
    public RootResponse(IProcess process, ProcessExecutionResult processExecutionResult)
    {
        Process = process;
        ProcessExecutionResult = processExecutionResult;
    }

    public IProcess Process { get; }

    public ProcessExecutionResult ProcessExecutionResult { get; }
}