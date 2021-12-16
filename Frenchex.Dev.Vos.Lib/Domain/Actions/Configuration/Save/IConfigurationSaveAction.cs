using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Save
{
    public interface IConfigurationSaveAction
    {
        Task Save(Bases.Configuration configuration, string path);
    }

    public class ConfigurationSaveAction : IConfigurationSaveAction
    {
        private readonly IFilesystem _fileSystemOperator;
        public ConfigurationSaveAction(
            IFilesystem fileSystem
        )
        {
            _fileSystemOperator = fileSystem;
        }

        public async Task Save(Bases.Configuration configuration, string path)
        {
            var serialized = JsonConvert.SerializeObject(configuration, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            await _fileSystemOperator.WriteAllTextAsync(path, serialized);
        }
    }

}
