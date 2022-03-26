using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public class HaltCommandResponseBuilder : RootResponseBuilder, IHaltCommandResponseBuilder
{
    private Vagrant.Lib.Domain.Commands.Halt.IHaltCommandResponse? _haltCommandResponse;

    public IHaltCommandResponse Build()
    {
        if (null == _haltCommandResponse)
            throw new InvalidOperationException("Halt command response is null");

        return new HaltCommandResponse(_haltCommandResponse);
    }

    public IHaltCommandResponseBuilder WithHaltResponse(Vagrant.Lib.Domain.Commands.Halt.IHaltCommandResponse response)
    {
        _haltCommandResponse = response;
        return this;
    }
}