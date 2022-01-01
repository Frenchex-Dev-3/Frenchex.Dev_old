using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

public class SshCommandResponseBuilderFactory : RootResponseBuilderFactory, ISshCommandResponseBuilderFactory
{
    public ISshCommandResponseBuilder Build()
    {
        return new SshCommandResponseBuilder();
    }
}