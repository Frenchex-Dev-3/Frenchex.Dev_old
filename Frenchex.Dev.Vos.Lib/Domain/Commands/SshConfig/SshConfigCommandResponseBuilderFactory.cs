using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandResponseBuilderFactory : RootResponseBuilderFactory,
    ISshConfigCommandResponseBuilderFactory
{
    public ISshConfigCommandResponseBuilder Build()
    {
        return new SshConfigCommandResponseBuilder();
    }
}