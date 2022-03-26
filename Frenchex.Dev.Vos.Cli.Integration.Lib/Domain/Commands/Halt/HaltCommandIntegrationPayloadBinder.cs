using System.CommandLine;
using System.CommandLine.Binding;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Halt;

public class HaltCommandIntegrationPayloadBinder : BinderBase<HaltCommandIntegrationPayload>
{
    private readonly Option<bool> _force;
    private readonly Option<int> _haltTimeoutMs;
    private readonly Argument<string[]> _names;
    private readonly Option<int> _timeoutMs;
    private readonly Option<string> _vagrantBinPath;
    private readonly Option<string> _workingDirectory;

    public HaltCommandIntegrationPayloadBinder(
        Argument<string[]> names,
        Option<bool> force,
        Option<int> haltTimeoutMs,
        Option<int> timeoutMs,
        Option<string> workingDirectory,
        Option<string> vagrantBinPath
    )
    {
        _names = names;
        _force = force;
        _haltTimeoutMs = haltTimeoutMs;
        _timeoutMs = timeoutMs;
        _workingDirectory = workingDirectory;
        _vagrantBinPath = vagrantBinPath;
    }

    protected override HaltCommandIntegrationPayload GetBoundValue(BindingContext bindingContext)
    {
        return new HaltCommandIntegrationPayload
        {
            Force = bindingContext.ParseResult.GetValueForOption(_force),
            HaltTimeoutMs = bindingContext.ParseResult.GetValueForOption(_haltTimeoutMs),
            Names = bindingContext.ParseResult.GetValueForArgument(_names),
            TimeoutMs = bindingContext.ParseResult.GetValueForOption(_timeoutMs),
            WorkingDirectory = bindingContext.ParseResult.GetValueForOption(_workingDirectory),
            VagrantBinPath = bindingContext.ParseResult.GetValueForOption(_vagrantBinPath)
        };
    }
}