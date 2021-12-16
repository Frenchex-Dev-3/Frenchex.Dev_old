using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class SharedFolderDefinition
    {
        [JsonProperty("host_path")]
        public string? HostPath { get; set; }

        [JsonProperty("guest_path")]
        public string? GuestPath { get; set; }

        [JsonProperty("enabled")]
        public bool? Enabled { get; set; }

        [JsonProperty("provider")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProviderEnum? Provider { get; set; }
    }
}
