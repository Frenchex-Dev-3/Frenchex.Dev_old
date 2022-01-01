using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;

public class DefineMachineAddCommandRequestBuilderFactory : IDefineMachineAddCommandRequestBuilderFactory
{
    private readonly IBaseRequestBuilderFactory _baseRequestBuilderFactory;

    public DefineMachineAddCommandRequestBuilderFactory(
        IBaseRequestBuilderFactory baseRequestBuilderFactory
    )
    {
        _baseRequestBuilderFactory = baseRequestBuilderFactory;
    }

    public IDefineMachineAddCommandRequestBuilder Factory()
    {
        return new DefineMachineAddCommandRequestBuilder(_baseRequestBuilderFactory);
    }
}