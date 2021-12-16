using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Create
{
    public interface IConfigurationCreateAction
    {
        Task Create(string path, Bases.Configuration? configuration = null);
    }

    public class ConfigurationCreateAction : IConfigurationCreateAction
    {
        private readonly IFilesystem _filesystem;

        public ConfigurationCreateAction(
            IFilesystem filesystem
        )
        {
            _filesystem = filesystem;
        }

        public async Task Create(string path, Bases.Configuration? configuration = null)
        {
            if (configuration is null)
            {
                configuration = new Bases.Configuration();
            }

            var newConfigSerializedJson = JsonConvert.SerializeObject(configuration, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            await _filesystem.WriteAllTextAsync(path, newConfigSerializedJson);
        }
    }
}
