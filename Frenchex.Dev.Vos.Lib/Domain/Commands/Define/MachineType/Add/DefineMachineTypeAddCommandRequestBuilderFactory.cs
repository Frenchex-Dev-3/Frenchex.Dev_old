using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;

public class DefineMachineTypeAddCommandRequestBuilderFactory : IDefineMachineTypeAddCommandRequestBuilderFactory
{
    private readonly IBaseRequestBuilderFactory _baseRequestBuilderFactory;

    public DefineMachineTypeAddCommandRequestBuilderFactory(
        IBaseRequestBuilderFactory baseRequestBuilderFactory
    )
    {
        _baseRequestBuilderFactory = baseRequestBuilderFactory;
    }

    public IDefineMachineTypeAddCommandRequestBuilder Factory()
    {
        return new DefineMachineTypeAddCommandRequestBuilder(_baseRequestBuilderFactory);
    }
}