using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public class Builder
{
    public static IProgram Build<T>(
        Action<IServiceCollection> servicesConfigurationLambda,
        string hostSettingsJsonFilename,
        string appSettingsJsonFilename,
        string envVarPrefix
    ) where T : class, IHostedService
    {
        var services = new ServiceCollection();

        new DependencyInjection.ServicesConfiguration()
            .ConfigureServices(services);

        var di = services.BuildServiceProvider();

        var scope = di.CreateAsyncScope();

        var scopedDi = scope.ServiceProvider;

        var programBuilder = scopedDi.GetRequiredService<IProgramBuilder>();

        var program = programBuilder.Build(
            new Context(
                hostSettingsJsonFilename, 
                appSettingsJsonFilename, 
                envVarPrefix,
                Directory.GetCurrentDirectory()
            ),
            programServices =>
            {
                servicesConfigurationLambda(programServices);
                programServices.AddHostedService<T>();
            }
        );

        return program;
    }
}