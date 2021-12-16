using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public interface IAppConfigurationConfiguration
    {
        void ConfigureApp(
            Context context,
            HostBuilderContext hostContext,
            IConfigurationBuilder appConfiguration
        );
    }

    public class AppConfigurationConfiguration : IAppConfigurationConfiguration
    {
        private readonly IEntrypointInfo _entrypointInfo;

        public AppConfigurationConfiguration(
            IEntrypointInfo entrypointInfo
        )
        {
            _entrypointInfo = entrypointInfo;
        }

        public void ConfigureApp(
            Context context,
            HostBuilderContext hostContext,
            IConfigurationBuilder appConfiguration
        )
        {
            appConfiguration.SetBasePath(context.BasePath);
            appConfiguration.AddJsonFile(context.AppSettings, optional: true);
            appConfiguration.AddJsonFile(
                $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                optional: true
            );
            appConfiguration.AddEnvironmentVariables(prefix: context.Prefix);
            appConfiguration.AddCommandLine(_entrypointInfo.CommandLineArgs);
        }
    }
}
