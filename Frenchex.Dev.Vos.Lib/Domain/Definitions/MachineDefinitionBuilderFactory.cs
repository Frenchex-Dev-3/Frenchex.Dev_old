namespace Frenchex.Dev.Vos.Lib.Domain.Definitions;

public class MachineDefinitionBuilderFactory
{
    private readonly MachineBaseDefinitionBuilderFactory _baseDefinitionBuilderFactory;

    public MachineDefinitionBuilderFactory(
        MachineBaseDefinitionBuilderFactory baseDefinitionBuilderFactory
    )
    {
        _baseDefinitionBuilderFactory = baseDefinitionBuilderFactory;
    }

    public MachineDefinitionBuilder Factory()
    {
        return new MachineDefinitionBuilder(_baseDefinitionBuilderFactory.Factory());
    }
}