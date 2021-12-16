using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class Configuration
    {
        [JsonProperty("vagrant")]
        public VagrantConfiguration Vagrant { get; set; } = new VagrantConfiguration();

        [JsonProperty("machine_types")]
        public Dictionary<string, MachineTypeDefinition> MachineTypes { get; set; } = new Dictionary<string, MachineTypeDefinition>();

        [JsonProperty("machines")]
        public Dictionary<string, MachineDefinition> Machines { get; set; } = new Dictionary<string, MachineDefinition>();
    }
}