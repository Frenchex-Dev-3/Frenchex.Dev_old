using Frenchex.Dev.Vos.Lib.Domain.Configuration;
using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Definitions;

public class MachineTypeDefinition
{
    [JsonProperty("base")] public MachineBaseDefinitionDeclaration? Base { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }
}