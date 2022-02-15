using System.CommandLine;
using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public enum ExitCode
{
    ExitNormal = 0,
    ExitGeneralException = 1
}

public abstract class AbstractHostedService : IHostedService
{
    private readonly IEntrypointInfo _entryPointInfo;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly IEnumerable<IIntegration> _integrations;
    private readonly ILogger<AbstractHostedService> _logger;
    private int _exitCode = (int)ExitCode.ExitNormal;

    protected RootCommand? RootCommand;

    protected AbstractHostedService(
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

        _hostApplicationLifetime.ApplicationStarted.Register(OnStartedAsync);
        _hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
        _hostApplicationLifetime.ApplicationStopped.Register(OnStopped);
    }

    private async void OnStartedAsync()
    {
        await OnStarted();
    }

    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected abstract Task OnStarted();

    protected abstract void OnStopping();

    protected abstract void OnStopped();

    protected async Task ExecuteAsync()
    {
        _logger.LogInformation("started");

        try
        {
            BuildAndAssignCommands();
            _exitCode = await ExecuteMainCommand();
        }
        catch (Exception e)
        {
            _logger.LogError("General exception not caught", e.Message);
            _exitCode |= (int)ExitCode.ExitGeneralException;
        }
        finally
        {
            _logger.LogInformation("finally stopping");
            _hostApplicationLifetime.StopApplication();
        }
    }

    private void BuildAndAssignCommands()
    {
        RootCommand = BuildRootCommand();
        BuildCommands();
    }

    protected void BuildCommands()
    {
        if (null == RootCommand)
            throw new ArgumentNullException(nameof(RootCommand));

        foreach (var integration in _integrations)
            integration.Integrate(RootCommand);
    }

    private RootCommand BuildRootCommand()
    {
        return new RootCommand(GetRootCommandDescription());
    }

    protected abstract string GetRootCommandDescription();

    private async Task<int> ExecuteMainCommand()
    {
        if (null == RootCommand)
            throw new ArgumentNullException(nameof(RootCommand));

        return await RootCommand.InvokeAsync(_entryPointInfo.CommandLineArgs);
    }
}