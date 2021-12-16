namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public interface IMachineDefinitionBuilderFactory
    {
        IMachineDefinitionBuilder Factory();
    }

    public class MachineDefinitionBuilderFactory : IMachineDefinitionBuilderFactory
    {
        private readonly IMachineBaseDefinitionBuilderFactory _baseDefinitionBuilderFactory;

        public MachineDefinitionBuilderFactory(
            IMachineBaseDefinitionBuilderFactory baseDefinitionBuilderFactory
        )
        {
            _baseDefinitionBuilderFactory = baseDefinitionBuilderFactory;
        }

        public IMachineDefinitionBuilder Factory()
        {
            return new MachineDefinitionBuilder(_baseDefinitionBuilderFactory.Factory());
        }
    }

    public interface IMachineDefinitionBuilder
    {
        IMachineBaseDefinitionBuilder BaseBuilder { get; }
        IMachineDefinitionBuilder WithName(string name);
        IMachineDefinitionBuilder WithNamingPattern(string namingPattern);
        IMachineDefinitionBuilder WithInstances(int instances);
        IMachineDefinitionBuilder Enabled(bool enabled);
        IMachineDefinitionBuilder IsPrimary(bool isPrimary);
        IMachineDefinitionBuilder WithVirtualCPUs(int virtualCPUs);
        IMachineDefinitionBuilder WithIPv4Pattern(string ipv4Pattern);
        IMachineDefinitionBuilder WithIPv4Start(int ipv4Start);
        IMachineDefinitionBuilder WithRamInMB(int ramInMB);
        IMachineDefinitionBuilder WithMachineType(string machineTypeDefinitionName);
        MachineDefinition Build();
    }

    public class MachineDefinitionBuilder : IMachineDefinitionBuilder
    {
        public IMachineBaseDefinitionBuilder BaseBuilder { get; }

        public MachineDefinitionBuilder(
                 IMachineBaseDefinitionBuilder baseBuilder
             )
        {
            BaseBuilder = baseBuilder.SetParent(this);
        }

        public MachineDefinition Build()
        {
            return new MachineDefinition(
                @base: BaseBuilder.Build(),
                machineTypeName: _machineTypeName,
                name: _name,
                namingPattern: _namingPattern,
                instances: _instances,
                ramInMB: _ramInMB,
                virtualCPUs: _vcpus,
                ipv4Pattern: _ipv4Pattern,
                ipv4Start: _ipv4Start,
                isPrimary: _isPrimary,
                isEnabled: _isEnabled
            );
        }

        private string? _machineTypeName;
        public IMachineDefinitionBuilder WithMachineType(string machineTypeDefinitionName)
        {
            _machineTypeName = machineTypeDefinitionName;
            return this;
        }

        private string? _name;
        public IMachineDefinitionBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        private string? _namingPattern;
        public IMachineDefinitionBuilder WithNamingPattern(string namingPattern)
        {
            _namingPattern = namingPattern;
            return this;
        }

        private int? _instances;
        public IMachineDefinitionBuilder WithInstances(int instances)
        {
            _instances = instances;
            return this;
        }

        private bool? _isEnabled;
        public IMachineDefinitionBuilder Enabled(bool enabled)
        {
            _isEnabled = enabled;
            return this;
        }

        private bool? _isPrimary;
        public IMachineDefinitionBuilder IsPrimary(bool isPrimary)
        {
            _isPrimary = isPrimary;
            return this;
        }

        private int? _vcpus;
        public IMachineDefinitionBuilder WithVirtualCPUs(int virtualCPUs)
        {
            _vcpus = virtualCPUs;
            return this;
        }

        private string? _ipv4Pattern;
        public IMachineDefinitionBuilder WithIPv4Pattern(string ipv4Pattern)
        {
            _ipv4Pattern = ipv4Pattern;
            return this;
        }

        private int? _ipv4Start;
        public IMachineDefinitionBuilder WithIPv4Start(int ipv4Start)
        {
            _ipv4Start = ipv4Start;
            return this;
        }

        private int? _ramInMB;
        public IMachineDefinitionBuilder WithRamInMB(int ramInMB)
        {
            _ramInMB = ramInMB;
            return this;
        }
    }
}
