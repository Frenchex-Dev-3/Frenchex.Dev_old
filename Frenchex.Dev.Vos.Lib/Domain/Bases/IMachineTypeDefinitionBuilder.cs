namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public interface IMachineTypeDefinitionBuilderFactory
    {
        IMachineTypeDefinitionBuilder Factory();
    }


    public interface IMachineTypeDefinitionBuilder
    {
        public IMachineBaseDefinitionBuilder BaseBuilder { get; }
        public IMachineTypeDefinitionBuilder WithName(string name);

        public MachineTypeDefinition Build();
    }
}
