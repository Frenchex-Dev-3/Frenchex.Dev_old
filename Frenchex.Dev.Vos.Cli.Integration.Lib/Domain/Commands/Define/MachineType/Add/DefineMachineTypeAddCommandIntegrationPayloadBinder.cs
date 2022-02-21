using System.CommandLine;
using System.CommandLine.Binding;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType.Add;

public class
    DefineMachineTypeAddCommandIntegrationPayloadBinder : BinderBase<DefineMachineTypeAddCommandIntegrationPayload>
{
    private readonly Argument<string> _boxName;
    private readonly Option<bool> _is3DEnabled;
    private readonly Option<int> _videoRamMb;
    private readonly Option<bool> _isEnabled;
    private readonly Argument<string> _name;
    private readonly Argument<string> _osType;
    private readonly Argument<string> _osVersion;
    private readonly Argument<int> _ramMb;
    private readonly Option<int> _timeoutMs;
    private readonly Argument<int> _vCpus;
    private readonly Option<string> _workingDir;

    public DefineMachineTypeAddCommandIntegrationPayloadBinder(Argument<string> name,
        Argument<string> boxName,
        Argument<int> vCpus,
        Argument<int> ramMb,
        Argument<string> osType,
        Argument<string> osVersion,
        Option<bool> isEnabled,
        Option<bool> is3DEnabled,
        Option<int> videoRamMb,
        Option<int> timeoutMs,
        Option<string> workingDir
    )
    {
        _name = name;
        _boxName = boxName;
        _isEnabled = isEnabled;
        _is3DEnabled = is3DEnabled;
        _videoRamMb = videoRamMb;
        _vCpus = vCpus;
        _ramMb = ramMb;
        _osType = osType;
        _osVersion = osVersion;
        _timeoutMs = timeoutMs;
        _workingDir = workingDir;
    }

    protected override DefineMachineTypeAddCommandIntegrationPayload GetBoundValue(BindingContext bindingContext)
    {
        return new()
        {
            BoxName = bindingContext.ParseResult.GetValueForArgument(_boxName),
            RamInMb = bindingContext.ParseResult.GetValueForArgument(_ramMb),
            Enabled = bindingContext.ParseResult.GetValueForOption(_isEnabled),
            Name = bindingContext.ParseResult.GetValueForArgument(_name),
            VCpus = bindingContext.ParseResult.GetValueForArgument(_vCpus),
            WorkingDirectory = bindingContext.ParseResult.GetValueForOption(_workingDir),
            Enable3D = bindingContext.ParseResult.GetValueForOption(_is3DEnabled),
            VideoRamInMb = bindingContext.ParseResult.GetValueForOption(_videoRamMb),
            TimeOutMs = bindingContext.ParseResult.GetValueForOption(_timeoutMs),
            OsType = bindingContext.ParseResult.GetValueForArgument(_osType),
            OsVersion = bindingContext.ParseResult.GetValueForArgument(_osVersion)
        };
    }
}