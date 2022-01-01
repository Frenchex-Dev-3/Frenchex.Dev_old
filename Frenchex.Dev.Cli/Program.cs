using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Frenchex.Dev.Dotnet.Cli.Lib.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServicesConfiguration = Frenchex.Dev.Cli.DependencyInjection.ServicesConfiguration;

await Builder
    .Build<Host>(
        services =>
        {
            new ServicesConfiguration()
                .ConfigureServices(services);

            return ""; // weird need
        },
        "hostsettings.json",
        "appsettings.json",
        "FRENCHEXDEV_"
    )
    .RunAsync();

internal class Host : BasicHostedService
{
    public Host(
        ILogger<AbstractHostedService> logger,
        IHostApplicationLifetime hostApplicationLifetime,
        IEntrypointInfo entryPointInfo,
        IEnumerable<IIntegration> integrations
    ) : base("Frenchex.Dev.Cli", logger, hostApplicationLifetime, entryPointInfo, integrations)
    {
    }
}