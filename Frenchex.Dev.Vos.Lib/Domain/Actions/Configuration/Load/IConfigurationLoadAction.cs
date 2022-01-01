using Newtonsoft.Json;

namespace Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;

public interface IConfigurationLoadAction
{
    Task<Domain.Configuration.Configuration> Load(string path);
}

public class ConfigurationLoadAction : IConfigurationLoadAction
{
    public async Task<Domain.Configuration.Configuration> Load(string path)
    {
        var loaded = await File.ReadAllTextAsync(path);
        Domain.Configuration.Configuration? deserialized = null;
        try
        {
            deserialized = JsonConvert.DeserializeObject<Domain.Configuration.Configuration>(loaded);
        }
        catch (Exception e)
        {
        }

        return deserialized;
    }
}