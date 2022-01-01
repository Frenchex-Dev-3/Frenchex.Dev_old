using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandResponseBuilderFactory : RootCommandResponseBuilderFactory,
    ISshConfigCommandResponseBuilderFactory
{
    public ISshConfigCommandResponseBuilder Build()
    {
        return new SshConfigCommandResponseBuilder();
    }
}