using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class VagrantConfiguration
    {
        [JsonProperty("prefix-with-dirbase")]
        public bool PrefixWithDirBase { get; set; } = false;

        [JsonProperty("numbering-format")]
        public string NumberingFormat { get; set; } = "{0,2:D2}";

        [JsonProperty("plugins")]
        public Dictionary<string, VagrantPluginsConfiguration> Plugins { get; set; } = new Dictionary<string, VagrantPluginsConfiguration>();

    }
}