using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class VagrantPluginsConfiguration : IVagrantPluginsConfiguration
    {
        [JsonProperty("version")]
        public string? Version { get; }

        [JsonProperty("enabled")]
        public bool? Enabled { get; }

        [JsonProperty("configuration")]
        public Dictionary<string, object>? Configuration { get; } = new Dictionary<string, object>();
    }
}