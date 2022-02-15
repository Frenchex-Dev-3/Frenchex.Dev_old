using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Configuration;

public class MachineDefinitionDeclaration
{
    [JsonProperty("base")] public MachineBaseDefinitionDeclaration? Base { get; set; }

    [JsonProperty("machine_type_name")] public string? MachineTypeName { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }

    [JsonProperty("naming_pattern")] public string? NamingPattern { get; set; } = "#VDI#-#NAME#-#INSTANCE#";

    [JsonProperty("instances")] public int? Instances { get; set; } = 1;

    [JsonProperty("ram_mb")] public int? RamInMb { get; set; } = 128;

    [JsonProperty("vcpus")] public int? VirtualCpus { get; set; } = 2;

    [JsonProperty("ipv4_pattern")] public string? Ipv4Pattern { get; set; } = "10.100.1.#NUMBER#";

    [JsonProperty("ipv4_start")] public int? Ipv4Start { get; set; } = 2;

    [JsonProperty("is_primary")] public bool? IsPrimary { get; set; } = false;

    [JsonProperty("is_enabled")] public bool? IsEnabled { get; set; } = true;

    [JsonProperty("network_bridge")] public string? NetworkBridge { get; set; }
}