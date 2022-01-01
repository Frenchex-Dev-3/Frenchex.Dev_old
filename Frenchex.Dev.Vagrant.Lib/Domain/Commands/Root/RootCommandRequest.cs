namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public class RootCommandRequest : IRootCommandRequest
{
    public RootCommandRequest(IBaseCommandRequest _base)
    {
        Base = _base;
    }

    public IBaseCommandRequest Base { get; }
}