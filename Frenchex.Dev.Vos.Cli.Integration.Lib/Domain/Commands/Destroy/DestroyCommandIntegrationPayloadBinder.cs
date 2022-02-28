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
    private readonly Option<string> _vagrantBinPath;

    public DestroyCommandIntegrationPayloadBinder(
        Argument<string[]> nameOpt,
        Option<bool> forceOpt,
        Option<bool> gracefulOpt,
        Option<int> timeoutMsOpt,
        Option<string> workingDirOpt,
        Option<string> vagrantBinPath
    )
    {
        _nameOpt = nameOpt;
        _forceOpt = forceOpt;
        _gracefulOpt = gracefulOpt;
        _timeoutMsOpt = timeoutMsOpt;
        _workingDirOpt = workingDirOpt;
        _vagrantBinPath = vagrantBinPath;
    }

    protected override DestroyCommandIntegrationPayload GetBoundValue(BindingContext bindingContext)
    {
        return new DestroyCommandIntegrationPayload
        {
            Force = bindingContext.ParseResult.GetValueForOption(_forceOpt),
            Graceful = bindingContext.ParseResult.GetValueForOption(_gracefulOpt),
            NameOrId = bindingContext.ParseResult.GetValueForArgument(_nameOpt),
            TimeoutMs = bindingContext.ParseResult.GetValueForOption(_timeoutMsOpt),
            WorkingDirectory = bindingContext.ParseResult.GetValueForOption(_workingDirOpt),
            VagrantBinPath = bindingContext.ParseResult.GetValueForOption(_vagrantBinPath),
        };
    }
}