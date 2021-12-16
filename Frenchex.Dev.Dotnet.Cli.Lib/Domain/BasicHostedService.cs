using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public class BasicHostedService : AbstractHostedService
    {
        private readonly string _rootCommandDescription;

        public BasicHostedService(
            string rootCommandDescription,
            ILogger<AbstractHostedService> logger,
            IHostApplicationLifetime hostApplicationLifetime,
            IEntrypointInfo entryPointInfo,
            IEnumerable<IIntegration> integrations
        ) : base(logger, hostApplicationLifetime, entryPointInfo, integrations)
        {
            _rootCommandDescription = rootCommandDescription;
        }

        protected override string GetRootCommandDescription()
        {
            return _rootCommandDescription;
        }

        protected override Task OnStarted()
        {
            return ExecuteAsync();
        }

        protected override void OnStopped()
        {

        }

        protected override void OnStopping()
        {

        }
    }
}