using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Configuration;

public class VagrantConfiguration
{
    [JsonProperty("prefix-with-dirbase")] public bool PrefixWithDirBase { get; set; }

    [JsonProperty("instance-number")] public int InstanceNumber { get; set; }

    [JsonProperty("zeroes")] public int Zeroes { get; set; } = 2;

    [JsonProperty("naming-pattern")] public string NamingPattern { get; set; } = "#MACHINE-NAME#-#MACHINE-INSTANCE#";

    [JsonProperty("plugins")] public Dictionary<string, VagrantPluginsConfiguration> Plugins { get; set; } = new();
}