using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;
using Frenchex.Dev.Vos.Lib.Domain.Configuration;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine.Add;

public class DefineMachineAddCommandIntegration : ABaseCommandIntegration, IDefineMachineAddCommandIntegration
{
    private readonly IDefineMachineAddCommand _command;
    private readonly IDefineMachineAddCommandRequestBuilderFactory _requestBuilderFactory;

    public DefineMachineAddCommandIntegration(
        IDefineMachineAddCommand command,
        IDefineMachineAddCommandRequestBuilderFactory responseBuilderFactory
    )
    {
        _command = command;
        _requestBuilderFactory = responseBuilderFactory;
    }

    public void Integrate(Command rootCommand)
    {
        var nameArg = new Argument<string>("name", "Name");
        var typeArg = new Argument<string>("type", "MachineType Name");
        var instancesArg = new Argument<int>("instances", "# of instances");
        var namingPatternOpt =
            new Option<string>(new[] {"--naming-pattern"}, () => "#vdi#-#name#-#instance#", "Naming Pattern");
        var isPrimaryOpt = new Option<bool>(new[] {"--primary"}, "Primary");
        var isEnabledOpt = new Option<bool>(new[] {"--enabled", "-e"}, "Enabled");
        var vCpusOpt = new Option<int>(new[] {"--vcpus", "-c"}, () => 2, "Virtual CPUs");
        var ramMbOpt = new Option<int>(new[] {"--ram", "-r"}, () => 128, "RAM in MB");
        var ipv4PatternOpt = new Option<string>(new[] {"--ipv4-pattern"}, () => "10.100.1.#INSTANCE#", "IPv4 pattern");
        var ipv4StartOpt = new Option<int>(new[] {"--ipv4-start"}, () => 2, "IPv4 start");
        var networkBridgeOpt = new Option<string>(new[] {"--network-bridge"}, "Network Bridge");
        var timeoutMsOpt = new Option<int>(new[] {"--timeout-ms", "-t"}, "TimeOut in ms");
        var workingDirOpt = new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory");

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