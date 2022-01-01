using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public interface IRootCommandResponseBuilder
{
    IRootCommandResponseBuilder SetProcess(IProcess process);
    IRootCommandResponseBuilder SetProcessExecutionResult(ProcessExecutionResult processExecutionResult);
}