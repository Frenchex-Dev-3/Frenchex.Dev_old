using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Definitions;

public class FileCopyDefinition
{
    public FileCopyDefinition(string hostSource, string guestTarget, bool enabled)
    {
        HostSource = hostSource;
        GuestTarget = guestTarget;
        Enabled = enabled;
    }

    [JsonProperty("host_source")] public string HostSource { get; }

    [JsonProperty("guest_target")] public string GuestTarget { get; }

    [JsonProperty("enabled")] public bool Enabled { get; }
}