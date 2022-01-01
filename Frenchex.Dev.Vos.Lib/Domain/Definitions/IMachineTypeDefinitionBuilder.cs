namespace Frenchex.Dev.Vos.Lib.Domain.Definitions;

public interface IMachineTypeDefinitionBuilder
{
    public MachineBaseDefinitionBuilder BaseBuilder { get; }
    public IMachineTypeDefinitionBuilder WithName(string name);

    public MachineTypeDefinition Build();
}