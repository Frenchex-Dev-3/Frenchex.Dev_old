using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;

public class DestroyCommandResponseBuilderFactory : RootCommandResponseBuilderFactory,
    IDestroyCommandResponseBuilderFactory
{
    public IDestroyCommandResponseBuilder Build()
    {
        return new DestroyCommandResponseBuilder();
    }
}