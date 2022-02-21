using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;
using Frenchex.Dev.Vos.Lib.Domain.Configuration;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine.Add;

public class DefineMachineAddCommandIntegration : ABaseCommandIntegration, IDefineMachineAddCommandIntegration
{
    private readonly IDefineMachineAddCommand _command;
    private readonly IEnabledOptionBuilder _enabledOptionBuilder;
    private readonly IInstancesArgumentBuilder _instancesArgumentBuilder;
    private readonly IIpv4PatternOptionBuilder _ipv4PatternOptionBuilder;
    private readonly IIpv4StartOptionBuilder _ipv4StartOptionBuilder;
    private readonly IMachineTypeNameArgumentBuilder _machineTypeNameArgumentBuilder;
    private readonly INameArgumentBuilder _nameArgumentBuilder;
    private readonly INamingPatternOptionBuilder _namingPatternOptionBuilder;
    private readonly INetworkBridgeOptionBuilder _networkBridgeOptionBuilder;
    private readonly IPrimaryOptionBuilder _primaryOptionBuilder;
    private readonly IRamMbOptionBuilder _ramMbOptionBuilder;
    private readonly IDefineMachineAddCommandRequestBuilderFactory _requestBuilderFactory;
    private readonly ITimeoutMsOptionBuilder _timeoutMsOptionBuilder;
    private readonly IVirtualCpusOptionBuilder _virtualCpusOptionBuilder;
    private readonly IWorkingDirectoryOptionBuilder _workingDirectoryOptionBuilder;

    public DefineMachineAddCommandIntegration(
        IDefineMachineAddCommand command,
        IDefineMachineAddCommandRequestBuilderFactory responseBuilderFactory,
        INameArgumentBuilder nameArgumentBuilder,
        IMachineTypeNameArgumentBuilder machineTypeNameArgumentBuilder,
        IInstancesArgumentBuilder instancesArgumentBuilder,
        INamingPatternOptionBuilder namingPatternOptionBuilder,
        IPrimaryOptionBuilder primaryOptionBuilder,
        IEnabledOptionBuilder enabledOptionBuilder,
        IVirtualCpusOptionBuilder virtualCpusOptionBuilder,
        IRamMbOptionBuilder ramMbOptionBuilder,
        IIpv4PatternOptionBuilder ipv4PatternOptionBuilder,
        IIpv4StartOptionBuilder ipv4StartOptionBuilder,
        INetworkBridgeOptionBuilder networkBridgeOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder
    )
    {
        _command = command;
        _requestBuilderFactory = responseBuilderFactory;
        _nameArgumentBuilder = nameArgumentBuilder;
        _machineTypeNameArgumentBuilder = machineTypeNameArgumentBuilder;
        _instancesArgumentBuilder = instancesArgumentBuilder;
        _namingPatternOptionBuilder = namingPatternOptionBuilder;
        _primaryOptionBuilder = primaryOptionBuilder;
        _enabledOptionBuilder = enabledOptionBuilder;
        _virtualCpusOptionBuilder = virtualCpusOptionBuilder;
        _ramMbOptionBuilder = ramMbOptionBuilder;
        _ipv4PatternOptionBuilder = ipv4PatternOptionBuilder;
        _ipv4StartOptionBuilder = ipv4StartOptionBuilder;
        _networkBridgeOptionBuilder = networkBridgeOptionBuilder;
        _timeoutMsOptionBuilder = timeoutMsOptionBuilder;
        _workingDirectoryOptionBuilder = workingDirectoryOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var nameArg = _nameArgumentBuilder.Build();
        var typeArg = _machineTypeNameArgumentBuilder.Build();
        var instancesArg = _instancesArgumentBuilder.Build();
        var namingPatternOpt = _namingPatternOptionBuilder.Build();

        var isPrimaryOpt = _primaryOptionBuilder.Build();
        var isEnabledOpt = _enabledOptionBuilder.Build();
        var vCpusOpt = _virtualCpusOptionBuilder.Build();
        var ramMbOpt = _ramMbOptionBuilder.Build();
        var ipv4PatternOpt = _ipv4PatternOptionBuilder.Build();
        var ipv4StartOpt = _ipv4StartOptionBuilder.Build();
        var networkBridgeOpt = _networkBridgeOptionBuilder.Build();
        var timeoutMsOpt = _timeoutMsOptionBuilder.Build();
        var workingDirOpt = _workingDirectoryOptionBuilder.Build();

        var command = new Command("add", "Add a new Machine")
        {
            nameArg,
            typeArg,
            instancesArg,
            namingPatternOpt,
            isPrimaryOpt,
            isEnabledOpt,
            vCpusOpt,
            ramMbOpt,
            ipv4PatternOpt,
            ipv4StartOpt,
            networkBridgeOpt,
            timeoutMsOpt,
            workingDirOpt
        };

        var binder = new DefineMachineAddCommandIntegrationPayloadBinder(
            nameArg,
            typeArg,
            instancesArg,
            namingPatternOpt,
            isPrimaryOpt,
            isEnabledOpt,
            vCpusOpt,
            ramMbOpt,
            ipv4PatternOpt,
            ipv4StartOpt,
            networkBridgeOpt,
            timeoutMsOpt,
            workingDirOpt
        );

        command.SetHandler(async (
            DefineMachineAddCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            var request = _requestBuilderFactory.Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds(payload.TimeoutMs)
                .UsingWorkingDirectory(payload.WorkingDirectory)
                .Parent<DefineMachineAddCommandRequestBuilder>()
                .UsingDefinition(new MachineDefinitionDeclaration
                {
                    Name = payload.Name,
                    VirtualCpus = payload.VCpus,
                    RamInMb = payload.RamInMb,
                    MachineTypeName = payload.Type,
                    Instances = payload.Instances,
                    IsEnabled = payload.Enabled,
                    Ipv4Pattern = payload.IPv4Pattern,
                    Ipv4Start = payload.IPv4Start,
                    IsPrimary = payload.IsPrimary,
                    NamingPattern = payload.NamingPattern,
                    NetworkBridge = payload.NetworkBridge
                })
                .Build();

            var response = await _command.Execute(request);
        }, binder);

        rootCommand.AddCommand(command);
    }
}