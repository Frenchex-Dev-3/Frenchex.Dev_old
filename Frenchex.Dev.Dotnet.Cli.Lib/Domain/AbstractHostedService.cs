using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.CommandLine;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public abstract class AbstractHostedService : IHostedService
    {
        private readonly ILogger<AbstractHostedService> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IEntrypointInfo _entryPointInfo;
        private readonly IEnumerable<IIntegration> _integrations;

        protected RootCommand? _rootCommand;

        public enum ExitCode : int
        {
            EXIT_NORMAL = 0,
            EXIT_GENERAL_EXCEPTION = 1
        }

        public AbstractHostedService(
            ILogger<AbstractHostedService> logger,
            IHostApplicationLifetime hostApplicationLifetime,
            IEntrypointInfo entryPointInfo,
            IEnumerable<IIntegration> integrations
        )
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
            _entryPointInfo = entryPointInfo;
            _integrations = integrations;

            _hostApplicationLifetime.ApplicationStarted.Register(async () => { await OnStarted(); });
            _hostApplicationLifetime.ApplicationStopping.Register(() => { OnStopping(); });
            _hostApplicationLifetime.ApplicationStopped.Register(() => { OnStopped(); });
        }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        abstract protected Task OnStarted();

        abstract protected void OnStopping();

        abstract protected void OnStopped();

        protected async Task ExecuteAsync()
        {
            _logger.LogInformation("started");
            var exitCode = 0;

            try
            {
                BuildAndAssignCommands();
                exitCode = await ExecuteMainCommand();
            }
            catch (Exception e)
            {
                _logger.LogError("General exception not caught", e);
                exitCode |= (int)ExitCode.EXIT_GENERAL_EXCEPTION;
            }
            finally
            {
                _logger.LogInformation("finally stopping");
                _hostApplicationLifetime.StopApplication();
            }
        }

        private void BuildAndAssignCommands()
        {
            _rootCommand = BuildRootCommand();
            BuildCommands();
        }

        protected void BuildCommands()
        {
            if (null == _rootCommand)
            {
                throw new ArgumentNullException(nameof(_rootCommand));
            }

            foreach (var integration in _integrations)
            {
                integration.Integrate(_rootCommand);
            }
        }

        private RootCommand BuildRootCommand()
        {
            return new RootCommand(description: GetRootCommandDescription());
        }

        protected abstract string GetRootCommandDescription();

        private async Task<int> ExecuteMainCommand()
        {
            if (null == _rootCommand)
            {
                throw new ArgumentNullException(nameof(_rootCommand));
            }

            return await _rootCommand.InvokeAsync(_entryPointInfo.CommandLineArgs);
        }
    }
}