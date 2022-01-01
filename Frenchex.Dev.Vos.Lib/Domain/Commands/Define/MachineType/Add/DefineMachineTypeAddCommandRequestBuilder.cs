using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;

public class DefineMachineTypeAddCommandRequestBuilder : IDefineMachineTypeAddCommandRequestBuilder
{
    private MachineTypeDefinition? _definition;

    public DefineMachineTypeAddCommandRequestBuilder(
        IBaseRequestBuilderFactory baseRequestBuilderFactory
    )
    {
        BaseBuilder = baseRequestBuilderFactory.Factory(this);
    }

    public IBaseRequestBuilder BaseBuilder { get; }


    public IDefineMachineTypeAddCommandRequestBuilder UsingDefinition(MachineTypeDefinition definition)
    {
        _definition = definition;
        return this;
    }

    public IDefineMachineTypeAddCommandRequest Build()
    {
        if (null == _definition) throw new InvalidOperationException("_definition is null");

        return new DefineMachineTypeAddCommandRequest(BaseBuilder.Build(), _definition);
    }
}