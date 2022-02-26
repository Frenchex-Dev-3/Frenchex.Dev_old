using Frenchex.Dev.Cli;
using Frenchex.Dev.Dotnet.Cli.Lib.Domain;
using Microsoft.Extensions.DependencyInjection;
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