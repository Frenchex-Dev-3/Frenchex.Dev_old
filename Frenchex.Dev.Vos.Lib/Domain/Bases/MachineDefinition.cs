using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class MachineDefinition
    {
        public MachineDefinition(
            MachineBaseDefinition? @base,
            string? machineTypeName,
            string? name,
            string? namingPattern,
            int? instances,
            int? ramInMB,
            int? virtualCPUs,
            string? ipv4Pattern,
            int? ipv4Start,
            bool? isPrimary,
            bool? isEnabled)
        {
            Base = @base;
            MachineTypeName = machineTypeName;
            Name = name;
            NamingPattern = namingPattern;
            Instances = instances;
            RamInMB = ramInMB;
            VirtualCPUs = virtualCPUs;
            Ipv4Pattern = ipv4Pattern;
            Ipv4Start = ipv4Start;
            IsPrimary = isPrimary;
            IsEnabled = isEnabled;
        }

        [JsonProperty("base")]
        public MachineBaseDefinition? Base { get; set; }

        [JsonProperty("machine_type_name")]
        public string? MachineTypeName { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("naming_pattern")]
        public string? NamingPattern { get; set; }

        [JsonProperty("instances")]
        public int? Instances { get; set; }

        [JsonProperty("ram_mb")]
        public int? RamInMB { get; set; }

        [JsonProperty("vcpus")]
        public int? VirtualCPUs { get; set; }

        [JsonProperty("ipv4_pattern")]
        public string? Ipv4Pattern { get; set; }

        [JsonProperty("ipv4_start")]
        public int? Ipv4Start { get; set; }

        [JsonProperty("is_primary")]
        public bool? IsPrimary { get; set; }

        [JsonProperty("is_enabled")]
        public bool? IsEnabled { get; set; }
    }
}
