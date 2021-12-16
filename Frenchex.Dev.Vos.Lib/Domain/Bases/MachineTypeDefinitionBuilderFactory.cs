namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class MachineTypeDefinitionBuilderFactory : IMachineTypeDefinitionBuilderFactory
    {
        private readonly IMachineBaseDefinitionBuilderFactory _baseDefinitionBuilderFactory;

        public MachineTypeDefinitionBuilderFactory(
            IMachineBaseDefinitionBuilderFactory baseDefinitionBuilderFactory
        )
        {
            _baseDefinitionBuilderFactory = baseDefinitionBuilderFactory;
        }

        public IMachineTypeDefinitionBuilder Factory()
        {
            return new MachineTypeDefinitionBuilder(_baseDefinitionBuilderFactory.Factory());
        }
    }
}
