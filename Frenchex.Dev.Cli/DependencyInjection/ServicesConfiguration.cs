using Frenchex.Dev.Dotnet.Cli.Lib.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Cli.DependencyInjection;

internal class ServicesConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        Vos.Cli.Integration.Lib.DependencyInjection.ServicesConfiguration
            .ConfigureServices(services);
    }
}