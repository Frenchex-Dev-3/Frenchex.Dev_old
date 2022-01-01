namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

public class RootRequest : IRootCommandRequest
{
    public RootRequest(IBaseRequest _base)
    {
        Base = _base;
    }

    public IBaseRequest Base { get; }
}