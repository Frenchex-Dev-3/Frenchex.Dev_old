using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public interface IProgramBuilder
{
    IProgram Build(
        Context context,
        Action<IServiceCollection> registerServices,
        Action<ILoggingBuilder> loggingConfiguration
    );
}