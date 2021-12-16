using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public interface IHostBuilder
    {
        IHost Build(Context context, Func<IServiceCollection, IServiceCollection> servicesConfigurationLambda);
    }
}
