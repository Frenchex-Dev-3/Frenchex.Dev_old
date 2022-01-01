using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Configuration;

public class VagrantPluginsConfiguration
{
    [JsonProperty("version")] public string? Version { get; }

    [JsonProperty("enabled")] public bool? Enabled { get; }

    [JsonProperty("configuration")] public Dictionary<string, object>? Configuration { get; } = new();
}