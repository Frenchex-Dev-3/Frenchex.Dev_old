using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

public class DestroyCommandResponseBuilder : RootResponseBuilder, IDestroyCommandResponseBuilder
{
    public IDestroyCommandResponse Build()
    {
        return new DestroyCommandResponse();
    }
}