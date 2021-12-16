using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root
{
    public class RootResponse : IRootCommandResponse
    {
        public IProcess Process { get; private set; }

        public ProcessExecutionResult ProcessExecutionResult { get; private set; }

        public RootResponse(IProcess process, ProcessExecutionResult processExecutionResult)
        {
            Process = process;
            ProcessExecutionResult = processExecutionResult;
        }
    }
}
