using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public class MachineTypeDefinition
    {
        [JsonProperty("base")]
        public MachineBaseDefinition Base { get; }

        [JsonProperty("name")]
        public string Name { get; }

        public MachineTypeDefinition(MachineBaseDefinition @base, string name)
        {
            Base = @base;
            Name = name;

            if (Base is null)
            {
                throw new ArgumentNullException(nameof(@base));
            }
        }
    }
}
