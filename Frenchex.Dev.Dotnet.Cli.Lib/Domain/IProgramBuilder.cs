using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public interface IProgramBuilder
    {
        IProgram Build(Context context, Func<IServiceCollection, IServiceCollection> registerServices);
    }
}