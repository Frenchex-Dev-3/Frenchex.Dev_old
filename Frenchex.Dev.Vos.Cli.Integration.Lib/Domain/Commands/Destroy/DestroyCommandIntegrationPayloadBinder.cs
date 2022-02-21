using System.CommandLine;
using System.CommandLine.Binding;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Destroy;

public class DestroyCommandIntegrationPayloadBinder : BinderBase<DestroyCommandIntegrationPayload>
{
    private readonly Option<bool> _forceOpt;
    private readonly Option<bool> _gracefulOpt;
    private readonly Argument<string[]> _nameOpt;
    private readonly Option<int> _timeoutMsOpt;
    private readonly Option<string> _workingDirOpt;

    public DestroyCommandIntegrationPayloadBinder(
        Argument<string[]> nameOpt,
        Option<bool> forceOpt,
        Option<bool> gracefulOpt,
        Option<int> timeoutMsOpt,
        Option<string> workingDirOpt
    )
    {
        _nameOpt = nameOpt;
        _forceOpt = forceOpt;
        _gracefulOpt = gracefulOpt;
        _timeoutMsOpt = timeoutMsOpt;
        _workingDirOpt = workingDirOpt;
    }

    protected override DestroyCommandIntegrationPayload GetBoundValue(BindingContext bindingContext)
    {
        return new DestroyCommandIntegrationPayload
        {
            TimeoutMs = bindingContext.ParseResult.GetValueForOption(_timeoutMsOpt),
            WorkingDirectory = bindingContext.ParseResult.GetValueForOption(_workingDirOpt),
            Force = bindingContext.ParseResult.GetValueForOption(_forceOpt),
            Graceful = bindingContext.ParseResult.GetValueForOption(_gracefulOpt),
            NameOrId = bindingContext.ParseResult.GetValueForArgument(_nameOpt)
        };
    }
}