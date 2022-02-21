using System.CommandLine;
using System.CommandLine.Binding;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine.Add;

public class DefineMachineAddCommandIntegrationPayloadBinder : BinderBase<DefineMachineAddCommandIntegrationPayload>
{
    private readonly Argument<int> _instances;
    private readonly Option<string> _ipv4Pattern;
    private readonly Option<int> _ipv4Start;
    private readonly Option<bool> _isEnabled;
    private readonly Option<bool> _isPrimary;
    private readonly Argument<string> _name;
    private readonly Option<string> _namingPattern;
    private readonly Option<string> _networkBridge;
    private readonly Option<int> _ramMb;
    private readonly Option<int> _timeoutMs;
    private readonly Argument<string> _type;
    private readonly Option<int> _vCpus;
    private readonly Option<string> _workingDir;

    public DefineMachineAddCommandIntegrationPayloadBinder(
        Argument<string> name,
        Argument<string> type,
        Argument<int> instances,
        Option<string> namingPattern,
        Option<bool> isPrimary,
        Option<bool> isEnabled,
        Option<int> vCpus,
        Option<int> ramMb,
        Option<string> ipv4Pattern,
        Option<int> ipv4Start,
        Option<string> networkBridge,
        Option<int> timeoutMs,
        Option<string> workingDir
    )
    {
        _name = name;
        _type = type;
        _instances = instances;
        _namingPattern = namingPattern;
        _isPrimary = isPrimary;
        _isEnabled = isEnabled;
        _vCpus = vCpus;
        _ramMb = ramMb;
        _ipv4Pattern = ipv4Pattern;
        _ipv4Start = ipv4Start;
        _networkBridge = networkBridge;
        _timeoutMs = timeoutMs;
        _workingDir = workingDir;
    }

    protected override DefineMachineAddCommandIntegrationPayload GetBoundValue(BindingContext bindingContext)
    {
        return new DefineMachineAddCommandIntegrationPayload
        {
            Type = bindingContext.ParseResult.GetValueForArgument(_type),
            RamInMb = bindingContext.ParseResult.GetValueForOption(_ramMb),
            Enabled = bindingContext.ParseResult.GetValueForOption(_isEnabled),
            IPv4Pattern = bindingContext.ParseResult.GetValueForOption(_ipv4Pattern),
            IPv4Start = bindingContext.ParseResult.GetValueForOption(_ipv4Start),
            Instances = bindingContext.ParseResult.GetValueForArgument(_instances),
            IsPrimary = bindingContext.ParseResult.GetValueForOption(_isPrimary),
            Name = bindingContext.ParseResult.GetValueForArgument(_name),
            NamingPattern = bindingContext.ParseResult.GetValueForOption(_namingPattern),
            NetworkBridge = bindingContext.ParseResult.GetValueForOption(_networkBridge),
            TimeoutMs = bindingContext.ParseResult.GetValueForOption(_timeoutMs),
            VCpus = bindingContext.ParseResult.GetValueForOption(_vCpus),
            WorkingDirectory = bindingContext.ParseResult.GetValueForOption(_workingDir)
        };
    }
}