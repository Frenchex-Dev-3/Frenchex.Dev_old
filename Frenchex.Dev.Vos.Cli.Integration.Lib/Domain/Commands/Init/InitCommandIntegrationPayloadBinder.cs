using System.CommandLine;
using System.CommandLine.Binding;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Init;

public class InitCommandIntegrationPayloadBinder : BinderBase<InitCommandIntegrationPayload>
{
    private readonly Option<string> _name;
    private readonly Option<int> _timeoutMs;
    private readonly Option<string> _workingDir;
    private readonly Option<int> _zeroes;

    public InitCommandIntegrationPayloadBinder(
        Option<string> name,
        Option<int> zeroes,
        Option<int> timeoutMs,
        Option<string> workingDir
    )
    {
        _name = name;
        _zeroes = zeroes;
        _timeoutMs = timeoutMs;
        _workingDir = workingDir;
    }

    protected override InitCommandIntegrationPayload GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            Naming = bindingContext.ParseResult.GetValueForOption(_name),
            TimeoutMs = bindingContext.ParseResult.GetValueForOption(_timeoutMs),
            WorkingDirectory = bindingContext.ParseResult.GetValueForOption(_workingDir),
            Zeroes = bindingContext.ParseResult.GetValueForOption(_zeroes)
        };
    }
}