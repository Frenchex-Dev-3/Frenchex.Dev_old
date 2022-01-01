using Frenchex.Dev.Vos.Lib.Domain.Definitions;
using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Configuration;

public class Configuration
{
    [JsonProperty("vagrant")] public VagrantConfiguration Vagrant { get; set; } = new();

    [JsonProperty("machine_types")] public Dictionary<string, MachineTypeDefinition> MachineTypes { get; set; } = new();

    [JsonProperty("machines")] public Dictionary<string, MachineDefinitionDeclaration> Machines { get; set; } = new();
}