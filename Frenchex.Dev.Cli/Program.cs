using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Frenchex.Dev.Dotnet.Cli.Lib.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServicesConfiguration = Frenchex.Dev.Cli.DependencyInjection.ServicesConfiguration;

await Builder
    .Build(
        services =>
        {
            services.AddHostedService<Host>();
            new ServicesConfiguration()
                .ConfigureServices(services);
        },
        logging => logging.ClearProviders().AddConsole(),
        "Configurations\\hostsettings.json",
        "Configurations\\appsettings.json",
        "FRENCHEXDEV_",
        AppDomain.CurrentDomain.BaseDirectory
    )
    .RunAsync();


/// <summary>
///     Implements BasicHostedService for this Program
/// </summary>
public class Host : BasicHostedService
{
    /// <summary>
    ///     Constructor for this Program Host
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="hostApplicationLifetime"></param>
    /// <param name="entryPointInfo"></param>
    /// <param name="integrations"></param>
    public Host(
        ILogger<AbstractHostedService> logger,
        IHostApplicationLifetime hostApplicationLifetime,
        IEntrypointInfo entryPointInfo,
        IEnumerable<IIntegration> integrations
    ) : base("Frenchex.Dev.Cli", logger, hostApplicationLifetime, entryPointInfo, integrations)
    {
    }
}