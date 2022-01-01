namespace Frenchex.Dev.Vos.Lib.Domain.Definitions;

public class MachineTypeDefinitionBuilder : IMachineTypeDefinitionBuilder
{
    private string? _name;

    public MachineTypeDefinitionBuilder(
        MachineBaseDefinitionBuilder baseBuilder
    )
    {
        BaseBuilder = baseBuilder.SetParent(this);
    }

    public MachineBaseDefinitionBuilder BaseBuilder { get; }

    public IMachineTypeDefinitionBuilder WithName(string name)
    {
        _name = name;
        return this;
    }


    public MachineTypeDefinition Build()
    {
        if (null == _name) throw new InvalidOperationException("name is null");

        return new MachineTypeDefinition {Base = BaseBuilder.Build(), Name = _name};
    }
}