using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

public abstract class RootCommand
{
    protected readonly IConfigurationLoadAction ConfigurationLoadAction;
    protected readonly IVexNameToVagrantNameConverter NameToVagrantNameConverter;

    protected RootCommand(
        IConfigurationLoadAction configurationLoadAction,
        IVexNameToVagrantNameConverter nameConverter
    )
    {
        ConfigurationLoadAction = configurationLoadAction;
        NameToVagrantNameConverter = nameConverter;
    }

    protected async Task<Configuration.Configuration> ConfigurationLoad(string? path)
    {
        return await ConfigurationLoadAction.Load(Path.Join(path, "config.json"));
    }

    protected string[] MapNamesToVagrantNames(
        string[] names,
        string? workingDirectory,
        Configuration.Configuration configuration
    )
    {
        return NameToVagrantNameConverter.ConvertAll(names, workingDirectory, configuration);
    }
}