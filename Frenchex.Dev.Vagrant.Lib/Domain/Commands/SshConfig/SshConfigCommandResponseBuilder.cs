using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig
{
    public class SshConfigCommandResponseBuilder : RootResponseBuilder, ISshConfigCommandResponseBuilder
    {
        public ISshConfigCommandResponse Build()
        {
            if (null == _process)
            {
                throw new InvalidOperationException("process is null");
            }

            if (null == _processExecutionResult)
            {
                throw new InvalidOperationException("processExecutionResult is null");
            }

            return new SshConfigCommandResponse(_process, _processExecutionResult);
        }
    }
}
