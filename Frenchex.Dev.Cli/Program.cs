using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Frenchex.Dev.Dotnet.Cli.Lib.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

await Builder
    .Build<Host>(
         (IServiceCollection services) =>
         {
             new Frenchex
                 .Dev
                 .Cli
                 .DependencyInjection
                 .ServicesConfiguration()
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