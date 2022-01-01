using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;

public class HaltCommandResponseBuilderFactory : RootCommandResponseBuilderFactory, IHaltCommandResponseBuilderFactory
{
    public IHaltCommandResponseBuilder Build()
    {
        return new HaltCommandResponseBuilder();
    }
}