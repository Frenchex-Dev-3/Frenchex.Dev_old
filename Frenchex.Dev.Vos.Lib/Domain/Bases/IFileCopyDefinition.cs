using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class FileCopyDefinition
    {
        [JsonProperty("host_source")]
        public string HostSource { get; }

        [JsonProperty("guest_target")]
        public string GuestTarget { get; }

        [JsonProperty("enabled")]
        public bool Enabled { get; }

        public FileCopyDefinition(string hostSource, string guestTarget, bool enabled)
        {
            HostSource = hostSource;
            GuestTarget = guestTarget;
            Enabled = enabled;
        }
    }
}
