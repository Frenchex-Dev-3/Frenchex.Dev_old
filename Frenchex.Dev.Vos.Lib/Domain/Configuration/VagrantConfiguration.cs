using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Configuration;

public class VagrantConfiguration
{
    [JsonProperty("prefix-with-dirbase")] public bool PrefixWithDirBase { get; set; }

    [JsonProperty("numbering-format")] public string NumberingFormat { get; set; } = "{0,2:D2}";

    [JsonProperty("naming-pattern")] public string NamingPattern { get; set; } = "#VDI-INSTANCE#-#NAME#-#INSTANCE#";

    [JsonProperty("plugins")] public Dictionary<string, VagrantPluginsConfiguration> Plugins { get; set; } = new();
}