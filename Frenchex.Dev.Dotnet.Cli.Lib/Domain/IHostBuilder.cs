using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public interface IHostBuilder
{
    IHost Build(
        Context context,
        Action<IServiceCollection> servicesConfigurationLambda,
        Action<ILoggingBuilder> loggingConfigurationLambda
    );
}