using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public class Builder
    {
        public static IProgram Build<T>(
            Func<IServiceCollection, string> servicesConfigurationLambda,
            string hostSettingsJsonFilename,
            string appSettingsJsonFilename,
            string envVarPrefix
        ) where T : class, IHostedService
        {
            var services = new ServiceCollection();

            servicesConfigurationLambda(services);

            var di = services.BuildServiceProvider();

            var programBuilder = di.GetRequiredService<IProgramBuilder>();

            var program = programBuilder.Build(
                new Context(hostSettingsJsonFilename, appSettingsJsonFilename, envVarPrefix, Directory.GetCurrentDirectory()),
                (IServiceCollection services) => services.AddHostedService<T>()
            );

            return program;
        }
    }
}
