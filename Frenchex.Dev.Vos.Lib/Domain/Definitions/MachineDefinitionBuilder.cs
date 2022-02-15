using Frenchex.Dev.Vos.Lib.Domain.Configuration;

namespace Frenchex.Dev.Vos.Lib.Domain.Definitions;

public class MachineDefinitionBuilder
{
    private int? _instances;

    private string? _ipv4Pattern;

    private int? _ipv4Start;

    private bool? _isEnabled;

    private bool? _isPrimary;

    private string? _machineTypeName;

    private string? _name;

    private string? _namingPattern;

    private string? _networkBridge;

    private int? _ramInMb;

    private int? _vcpus;

    public MachineDefinitionBuilder(
        MachineBaseDefinitionBuilder baseBuilder
    )
    {
        BaseBuilder = baseBuilder.SetParent(this);
    }

    public MachineBaseDefinitionBuilder BaseBuilder { get; }

    public MachineDefinitionDeclaration Build()
    {
        return new MachineDefinitionDeclaration
        {
            Base = BaseBuilder.Build(),
            MachineTypeName = _machineTypeName,
            Name = _name,
            NamingPattern = _namingPattern,
            Instances = _instances,
            RamInMb = _ramInMb,
            VirtualCpus = _vcpus,
            Ipv4Pattern = _ipv4Pattern,
            Ipv4Start = _ipv4Start,
            IsPrimary = _isPrimary,
            IsEnabled = _isEnabled,
            NetworkBridge = _networkBridge
        };
    }

    public MachineDefinitionBuilder WithMachineType(string machineTypeDefinitionName)
    {
        _machineTypeName = machineTypeDefinitionName;
        return this;
    }

    public MachineDefinitionBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public MachineDefinitionBuilder WithNamingPattern(string namingPattern)
    {
        _namingPattern = namingPattern;
        return this;
    }

    public MachineDefinitionBuilder WithInstances(int instances)
    {
        _instances = instances;
        return this;
    }

    public MachineDefinitionBuilder Enabled(bool enabled)
    {
        _isEnabled = enabled;
        return this;
    }

    public MachineDefinitionBuilder IsPrimary(bool isPrimary)
    {
        _isPrimary = isPrimary;
        return this;
    }

    public MachineDefinitionBuilder WithVirtualCpUs(int virtualCpUs)
    {
        if (virtualCpUs <= 0) throw new ArgumentOutOfRangeException(nameof(virtualCpUs));
        _vcpus = virtualCpUs;
        return this;
    }

    public MachineDefinitionBuilder WithIPv4Pattern(string ipv4Pattern)
    {
        _ipv4Pattern = ipv4Pattern;
        return this;
    }

    public MachineDefinitionBuilder WithIPv4Start(int ipv4Start)
    {
        _ipv4Start = ipv4Start;
        return this;
    }

    public MachineDefinitionBuilder WithRamInMb(int ramInMb)
    {
        _ramInMb = ramInMb;
        return this;
    }

    public MachineDefinitionBuilder WithNetworkBridge(string? networkBridge)
    {
        _networkBridge = networkBridge;
        return this;
    }
}