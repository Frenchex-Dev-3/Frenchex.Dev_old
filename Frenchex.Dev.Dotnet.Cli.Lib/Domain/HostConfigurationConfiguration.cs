using Microsoft.Extensions.Configuration;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public interface IHostConfigurationConfiguration
{
    void Configure(
        Context context,
        IConfigurationBuilder hostConfiguration
    );
}

public class HostConfigurationConfiguration : IHostConfigurationConfiguration
{
    private readonly IEntrypointInfo _entrypointInfo;

    public HostConfigurationConfiguration(
        IEntrypointInfo entrypointInfo
    )
    {
        _entrypointInfo = entrypointInfo;
    }

    public void Configure(
        Context context,
        IConfigurationBuilder hostConfiguration
    )
    {
        hostConfiguration
            .SetBasePath(context.BasePath)
            .AddJsonFile(context.HostSettings, true)
            .AddEnvironmentVariables(context.Prefix)
            .AddCommandLine(_entrypointInfo.CommandLineArgs)
            ;
    }
}