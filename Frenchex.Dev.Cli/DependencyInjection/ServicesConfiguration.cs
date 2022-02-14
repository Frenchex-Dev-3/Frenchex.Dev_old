using Frenchex.Dev.Dotnet.Cli.Lib.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Cli.DependencyInjection;

/// <summary>
/// Logic to configure an IServiceCollection with Vos CLI & Library dependencies registered as scoped.
/// </summary>
public class ServicesConfiguration : IServicesConfiguration
{
    /// <summary>
    /// Only method to run the logic on given `services` object
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        Vos.Cli.Integration.Lib.DependencyInjection.ServicesConfiguration
            .ConfigureServices(services);
    }
}