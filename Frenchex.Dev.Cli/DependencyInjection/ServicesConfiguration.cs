using Frenchex.Dev.Dotnet.Cli.Lib.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Cli.DependencyInjection;

internal class ServicesConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        new Dotnet.Cli.Lib.DependencyInjection.ServicesConfiguration()
            .ConfigureServices(services);
    }
}