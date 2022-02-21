using System.CommandLine;
using System.CommandLine.Binding;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Up;

public class UpCommandIntegrationPayloadBinder : BinderBase<UpCommandIntegrationPayload>
{
    private readonly Option<bool> _destroyOnError;
    private readonly Option<bool> _installProvider;
    private readonly Argument<string[]> _names;
    private readonly Option<bool> _parallel;
    private readonly Option<string> _provider;
    private readonly Option<bool> _provision;
    private readonly Option<string[]> _provisionWith;
    private readonly Option<int> _timeoutMs;
    private readonly Option<string> _workingDir;

    public UpCommandIntegrationPayloadBinder(
        Argument<string[]> names,
        Option<bool> provision,
        Option<string[]> provisionWith,
        Option<bool> destroyOnError,
        Option<bool> parallel,
        Option<string> provider,
        Option<bool> installProvider,
        Option<int> timeoutMs,
        Option<string> workingDir
    )
    {
        _names = names;
        _provision = provision;
        _provisionWith = provisionWith;
        _destroyOnError = destroyOnError;
        _parallel = parallel;
        _provider = provider;
        _installProvider = installProvider;
        _timeoutMs = timeoutMs;
        _workingDir = workingDir;
    }

    protected override UpCommandIntegrationPayload GetBoundValue(BindingContext bindingContext)
    {
        return new UpCommandIntegrationPayload
        {
            TimeoutMs = bindingContext.ParseResult.GetValueForOption(_timeoutMs),
            WorkingDirectory = bindingContext.ParseResult.GetValueForOption(_workingDir),
            DestroyOnError = bindingContext.ParseResult.GetValueForOption(_destroyOnError),
            InstallProvider = bindingContext.ParseResult.GetValueForOption(_installProvider),
            Names = bindingContext.ParseResult.GetValueForArgument(_names),
            Parallel = bindingContext.ParseResult.GetValueForOption(_parallel),
            Provider = bindingContext.ParseResult.GetValueForOption(_provider),
            Provision = bindingContext.ParseResult.GetValueForOption(_provision),
            ProvisionWith = bindingContext.ParseResult.GetValueForOption(_provisionWith)
        };
    }
}