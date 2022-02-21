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

        var deserialized = new Domain.Configuration.Configuration();

        try
        {
            deserialized = JsonConvert.DeserializeObject<Domain.Configuration.Configuration>(loaded);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while deserializing: {e.Message}");
        }

        if (null == deserialized) throw new ApplicationException("Error while deserializing");

        return deserialized;
    }
}