using Frenchex.Dev.Dotnet.Process.Lib.Domain.Process;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root
{
    abstract public class RootResponseBuilder : IRootCommandResponseBuilder
    {
        protected IProcess? _process;
        protected ProcessExecutionResult? _processExecutionResult;

        public IRootCommandResponseBuilder SetProcess(IProcess process)
        {
            _process = process;
            return this;
        }

        public IRootCommandResponseBuilder SetProcessExecutionResult(ProcessExecutionResult processExecutionResult)
        {
            _processExecutionResult = processExecutionResult;
            return this;
        }
    }
}
