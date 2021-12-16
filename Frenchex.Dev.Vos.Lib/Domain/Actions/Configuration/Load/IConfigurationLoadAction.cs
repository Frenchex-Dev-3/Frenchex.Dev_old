using Frenchex.Dev.Vos.Lib.Domain.Bases;
using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load
{
    public interface IConfigurationLoadAction
    {
        Task<Bases.Configuration> Load(string path);
    }

    public class ConfigurationLoadAction : IConfigurationLoadAction
    {
        public async Task<Bases.Configuration> Load(string path)
        {
            var loaded = await File.ReadAllTextAsync(path);
            var deserialized = JsonConvert.DeserializeObject<Bases.Configuration>(loaded);

            if (deserialized == null)
            {
                deserialized = new Bases.Configuration();
            }

            if (deserialized.Machines is null)
            {
                deserialized.Machines = new Dictionary<string, MachineDefinition>();
            }

            if (deserialized.MachineTypes is null)
            {
                deserialized.MachineTypes = new Dictionary<string, MachineTypeDefinition>();
            }

            return deserialized;
        }
    }
}
