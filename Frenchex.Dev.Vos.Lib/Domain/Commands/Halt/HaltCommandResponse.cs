using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public class HaltCommandResponse : RootResponse, IHaltCommandResponse
{
    public HaltCommandResponse(
        Vagrant.Lib.Domain.Commands.Halt.IHaltCommandResponse haltCommandResponse
    )
    {
        Response = haltCommandResponse;
    }

    public Vagrant.Lib.Domain.Commands.Halt.IHaltCommandResponse Response { get; }
}