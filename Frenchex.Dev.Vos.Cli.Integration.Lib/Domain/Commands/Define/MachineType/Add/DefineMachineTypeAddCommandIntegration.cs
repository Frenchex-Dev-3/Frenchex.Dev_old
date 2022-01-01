using System.CommandLine;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType.Add;

public class DefineMachineTypeAddCommandIntegration : ABaseCommandIntegration, IDefineMachineTypeAddCommandIntegration
{
    private readonly IDefineMachineTypeAddCommand _command;
    private readonly IMachineTypeDefinitionBuilderFactory _machineTypeDefinitionBuilder;
    private readonly IDefineMachineTypeAddCommandRequestBuilderFactory _requestBuilderFactory;

    public DefineMachineTypeAddCommandIntegration(
        IDefineMachineTypeAddCommand command,
        IDefineMachineTypeAddCommandRequestBuilderFactory responseBuilderFactory,
        IMachineTypeDefinitionBuilderFactory machineTypeDefinitionBuilderFactory
    )
    {
        _command = command;
        _requestBuilderFactory = responseBuilderFactory;
        _machineTypeDefinitionBuilder = machineTypeDefinitionBuilderFactory;
    }

    public void Integrate(Command rootCommand)
    {
        var command = new Command("add", "Define Machine-Types")
        {
            new Argument<string>("name", "Name"),
            new Argument<string>("box-name", "Box Name"),
            new Argument<int>("vcpus", "Virtual CPUs"),
            new Argument<int>("ram-in-mb", "RAM in MB"),
            new Option<bool>(new[] {"--enabled", "-e"}, "Enable Machine Type"),
            new Option<bool>(new[] {"--enable3d"}, "Enable 3D"),
            new Option<int>(new[] {"--timeoutms", "-t"}, "TimeOut in ms"),
            new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory")
        };

        command.Handler = CommandHandler.Create(async (
            string name,
            string boxName,
            int vCpus,
            int ramInMb,
            bool enabled,
            bool enable3D,
            int timeOutMiliseconds,
            string workingDirectory
        ) =>
        {
            var request = _requestBuilderFactory.Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds(timeOutMiliseconds)
                .UsingWorkingDirectory(workingDirectory)
                .Parent<IDefineMachineTypeAddCommandRequestBuilder>()
                .UsingDefinition(_machineTypeDefinitionBuilder.Factory()
                    .BaseBuilder
                    .Enabled(enabled)
                    .With3DEnabled(enable3D)
                    .WithBox(boxName)
                    .WithRamInMb(ramInMb)
                    .WithVirtualCpus(vCpus)
                    .Parent<IMachineTypeDefinitionBuilder>()
                    .WithName(name)
                    .Build()
                )
                .Build();

            var response = await _command.Execute(request);
        });

        rootCommand.AddCommand(command);
    }
}