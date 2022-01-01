using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public class HaltCommandResponseBuilder : RootResponseBuilder, IHaltCommandResponseBuilder
{
    public IHaltCommandResponse Build()
    {
        return new HaltCommandResponse();
    }
}