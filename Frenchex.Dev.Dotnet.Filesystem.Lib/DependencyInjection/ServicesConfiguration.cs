using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Dotnet.Filesystem.Lib.DependencyInjection;

public class ServicesConfiguration
{
    public static IServiceCollection ConfigureServices(IServiceCollection services)
    {
        services
            .AddScoped<IFilesystem, Domain.Filesystem>()
            ;

        return services;
    }
}