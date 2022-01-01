using System.CommandLine;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;
using Frenchex.Dev.Vos.Lib.Domain.Configuration;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine.Add;

public interface IDefineMachineAddCommandIntegration : IDefineMachineSubCommandIntegration
{
}

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
        var command = new Command("add", "Add a new Machine")
        {
            new Argument<string>("name", "Name"),
            new Argument<string>("type", "MachineType Name"),
            new Argument<int>("instances", "# of instances"),
            new Option<string>(new[] {"--naming-pattern"}, () => "#vdi#-#name#-#instance#", "Naming Pattern"),
            new Option<bool>(new[] {"--primary"}, "Primary"),
            new Option<bool>(new[] {"--enabled", "-e"}, "Enabled"),
            new Option<int>(new[] {"--vcpus", "-c"}, () => 2, "Virtual CPUs"),
            new Option<int>(new[] {"--ram", "-r"}, () => 128, "RAM in MB"),
            new Option<string>(new[] {"--ipv4-pattern"}, () => "10.100.1.#INSTANCE#", "IPv4 pattern"),
            new Option<int>(new[] {"--ipv4-start"}, () => 2, "IPv4 start"),
            new Option<string>(new[] {"--network-bridge"}, "Network Bridge"),
            new Option<int>(new[] {"--timeoutms", "-t"}, "TimeOut in ms"),
            new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory")
        };

        command.Handler = CommandHandler.Create(async (
            string name,
            string type,
            int instances,
            string namingPattern,
            bool? isPrimary,
            bool? enabled,
            int? virtualCpus,
            int? ramInMb,
            string? ipv4Pattern,
            int? ipv4Start,
            string? networkBridge,
            int timeOutMiliseconds,
            string workingDirectory
        ) =>
        {
            var request = _requestBuilderFactory.Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds(timeOutMiliseconds)
                .UsingWorkingDirectory(workingDirectory)
                .Parent<DefineMachineAddCommandRequestBuilder>()
                .UsingDefinition(new MachineDefinitionDeclaration
                {
                    Name = name,
                    VirtualCpus = virtualCpus,
                    RamInMB = ramInMb,
                    MachineTypeName = type,
                    Instances = instances,
                    IsEnabled = enabled,
                    Ipv4Pattern = ipv4Pattern,
                    Ipv4Start = ipv4Start,
                    IsPrimary = isPrimary,
                    NamingPattern = namingPattern,
                    NetworkBridge = networkBridge
                })
                .Build();

            var response = await _command.Execute(request);
        });

        rootCommand.AddCommand(command);
    }
}