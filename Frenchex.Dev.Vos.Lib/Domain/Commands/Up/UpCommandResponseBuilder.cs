using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up;

public class UpCommandResponseBuilder : RootResponseBuilder, IUpCommandResponseBuilder
{
    private Vagrant.Lib.Domain.Commands.Up.IUpCommandResponse? _upCommandResponse;

    public IUpCommandResponse Build()
    {
        if (null == _upCommandResponse) throw new InvalidOperationException("Up command response is null");

        return new UpCommandResponse(_upCommandResponse);
    }

    public IUpCommandResponseBuilder WithUpResponse(Vagrant.Lib.Domain.Commands.Up.IUpCommandResponse response)
    {
        _upCommandResponse = response;
        return this;
    }
}