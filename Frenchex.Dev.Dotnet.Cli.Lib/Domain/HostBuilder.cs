using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public class HostBuilder : IHostBuilder
    {
        private readonly IEntrypointInfo _entryPointInfo;
        private readonly IHostConfigurationConfiguration _hostConfiguration;
        private readonly IAppConfigurationConfiguration _appConfiguration;
        private readonly IServicesConfiguration _servicesConfiguration;

        public HostBuilder(
            IEntrypointInfo entrypointInfo,
            IHostConfigurationConfiguration hostConfigurationConfiguration,
            IAppConfigurationConfiguration appConfigurationConfiguration,
            IServicesConfiguration servicesConfiguration
        )
        {
            _entryPointInfo = entrypointInfo;
            _hostConfiguration = hostConfigurationConfiguration;
            _appConfiguration = appConfigurationConfiguration;
            _servicesConfiguration = servicesConfiguration;
        }

        public IHost Build(Context context, Func<IServiceCollection, IServiceCollection> servicesConfigurationLambda)
        {
            return Host
           .CreateDefaultBuilder(_entryPointInfo.CommandLineArgs)
           .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
           .ConfigureHostConfiguration((hostConfiguration) =>
           {
               _hostConfiguration.Configure(context, hostConfiguration);
           })
           .ConfigureAppConfiguration((hostContext, appConfiguration) =>
           {
               _appConfiguration.ConfigureApp(context, hostContext, appConfiguration);
           })
           .ConfigureServices((services) =>
           {
               servicesConfigurationLambda(services);
               _servicesConfiguration.ConfigureServices(services);
           })
           .Build()
           ;
        }
    }
}
