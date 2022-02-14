using System.CommandLine;
using System.CommandLine.Binding;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Status;

public class StatusCommandIntegrationPayloadBinder : BinderBase<StatusCommandIntegrationPayload>
{
    private readonly Argument<string[]> _nameArg;
    private readonly Option<int> _timeoutOpt;
    private readonly Option<string> _workingDirOpt;

    public StatusCommandIntegrationPayloadBinder(
        Argument<string[]> nameArg,
        Option<string> workingDirOpt,
        Option<int> timeoutOpt
    )
    {
        _nameArg = nameArg;
        _workingDirOpt = workingDirOpt;
        _timeoutOpt = timeoutOpt;
    }

    protected override StatusCommandIntegrationPayload GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            WorkingDirectory = bindingContext.ParseResult.GetValueForOption(_workingDirOpt),
            TimeoutMs = bindingContext.ParseResult.GetValueForOption(_timeoutOpt),
            Names = bindingContext.ParseResult.GetValueForArgument(_nameArg)
        };
    }
}