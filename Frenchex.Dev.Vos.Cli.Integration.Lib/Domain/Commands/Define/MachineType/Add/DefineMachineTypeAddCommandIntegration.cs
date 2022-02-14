using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;
using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;

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
        var nameArg = new Argument<string>("name", "Name");
        var boxNameArg = new Argument<string>("box-name", "Box Name");
        var vcpusArg = new Argument<int>("vcpus", "Virtual CPUs");
        var ramMbArg = new Argument<int>("ram-mb", "RAM in MB");
        var osTypeArg = new Argument<string>("os-type", "OS Name");
        var osVersionArg = new Argument<string>("os-version", "OS Version");
        var isEnabledOpt = new Option<bool>(new[] {"--enabled", "-e"}, "Enable Machine Type");
        var isEnabled3dOpt = new Option<bool>(new[] {"--enable3d"}, "Enable 3D");
        var timeoutMsOpt = new Option<int>(new[] {"--timeout-ms", "-t"}, "TimeOut in ms");
        var workingDirOpt = new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory");

        var command = new Command("add", "Define Machine-Types")
        {
            nameArg,
            boxNameArg,
            vcpusArg,
            ramMbArg,
            osTypeArg,
            osVersionArg,
            isEnabledOpt,
            isEnabled3dOpt,
            timeoutMsOpt,
            workingDirOpt
        };

        var binder = new DefineMachineTypeAddCommandIntegrationPayloadBinder(
            nameArg,
            boxNameArg,
            vcpusArg,
            ramMbArg,
            osTypeArg,
            osVersionArg,
            isEnabledOpt,
            isEnabled3dOpt,
            timeoutMsOpt,
            workingDirOpt
        );

        command.SetHandler(async(
            DefineMachineTypeAddCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            var request = _requestBuilderFactory.Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds(payload.TimeOutMs)
                .UsingWorkingDirectory(payload.WorkingDirectory)
                .Parent<IDefineMachineTypeAddCommandRequestBuilder>()
                .UsingDefinition(_machineTypeDefinitionBuilder.Factory()
                    .BaseBuilder
                    .Enabled(payload.Enabled)
                    .With3DEnabled(payload.Enable3D)
                    .WithBox(payload.BoxName)
                    .WithRamInMb(payload.RamInMb)
                    .WithVirtualCpus(payload.VCpus)
                    .WithOsType(Enum.Parse<OsTypeEnum>(payload.OsType))
                    .Parent<IMachineTypeDefinitionBuilder>()
                    .WithName(payload.Name)
                    .Build()
                )
                .Build();

            var response = await _command.Execute(request);
        }, binder);

        rootCommand.AddCommand(command);
    }
}