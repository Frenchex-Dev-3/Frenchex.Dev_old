using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

public class DestroyCommandResponseBuilderFactory : RootResponseBuilderFactory, IDestroyCommandResponseBuilderFactory
{
    public IDestroyCommandResponseBuilder Build()
    {
        return new DestroyCommandResponseBuilder();
    }
}