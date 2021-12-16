namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class MachineTypeDefinitionBuilder : IMachineTypeDefinitionBuilder
    {
        public MachineTypeDefinitionBuilder(
            IMachineBaseDefinitionBuilder baseBuilder
        )
        {
            BaseBuilder = baseBuilder.SetParent(this);
        }

        public IMachineBaseDefinitionBuilder BaseBuilder { get; }

        private string? _name;
        public IMachineTypeDefinitionBuilder WithName(string name)
        {
            _name = name;
            return this;
        }


        public MachineTypeDefinition Build()
        {
            if (null == _name)
            {
                throw new InvalidOperationException("name is null");
            }

            return new MachineTypeDefinition(BaseBuilder.Build(), _name);
        }
    }
}
