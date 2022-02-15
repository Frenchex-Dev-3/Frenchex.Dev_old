using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public class Builder
{
    public static IProgram Build(
        Action<IServiceCollection> configureProgramServicesAction,
        Action<ILoggingBuilder> configureProgramLoggingAction,
        string hostSettingsJsonFilename,
        string appSettingsJsonFilename,
        string envVarPrefix,
        string appDomainDirectory
    )
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
                Path.GetFullPath(hostSettingsJsonFilename, appDomainDirectory),
                Path.GetFullPath(appSettingsJsonFilename, appDomainDirectory),
                envVarPrefix,
                Directory.GetCurrentDirectory()
            ),
            configureProgramServicesAction,
            configureProgramLoggingAction
        );

        return program;
    }
}