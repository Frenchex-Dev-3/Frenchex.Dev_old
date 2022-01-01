using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;

public class SshCommandResponseBuilderFactory : RootCommandResponseBuilderFactory, ISshCommandResponseBuilderFactory
{
    public ISshCommandResponseBuilder Build()
    {
        return new SshCommandResponseBuilder();
    }
}