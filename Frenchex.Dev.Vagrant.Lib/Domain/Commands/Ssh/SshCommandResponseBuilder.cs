using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh
{
    public class SshCommandResponseBuilder : RootResponseBuilder, ISshCommandResponseBuilder
    {
        public ISshCommandResponse Build()
        {
            if (null == _process || null == _processExecutionResult)
            {
                throw new InvalidOperationException("process or processexecutionresult is null");
            }

            return new SshCommandResponse(_process, _processExecutionResult);
        }
    }
}
