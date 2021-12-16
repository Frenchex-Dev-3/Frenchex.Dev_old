using Microsoft.Extensions.DependencyInjection;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public class ProgramBuilder : IProgramBuilder
    {
        private readonly IHostBuilder _hostBuilder;

        public ProgramBuilder(
            IHostBuilder hostBuilder
        )
        {
            _hostBuilder = hostBuilder;
        }

        public IProgram Build(Context context, Func<IServiceCollection, IServiceCollection> registerServices)
        {
            return new Program(_hostBuilder.Build(context, registerServices));
        }
    }
}