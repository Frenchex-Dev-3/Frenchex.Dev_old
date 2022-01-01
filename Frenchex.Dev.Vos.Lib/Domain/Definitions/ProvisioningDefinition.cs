using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Definitions;

public class ProvisioningDefinition
{
    [JsonProperty("env")]
#pragma warning disable CA1822 // Marquer les membres comme étant static
    public Dictionary<string, string>? Env => new();
#pragma warning restore CA1822 // Marquer les membres comme étant static
}