using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType.Add;

public class DefineMachineTypeAddCommandIntegration : ABaseCommandIntegration, IDefineMachineTypeAddCommandIntegration
{
    private readonly IBoxNameArgumentBuilder _boxNameArgumentBuilder;
    private readonly IDefineMachineTypeAddCommand _command;
    private readonly IEnabled3dOptionBuilder _enabled3dOptionBuilder;
    private readonly IEnabledOptionBuilder _enabledOptionBuilder;
    private readonly IMachineTypeDefinitionBuilderFactory _machineTypeDefinitionBuilder;
    private readonly INameArgumentBuilder _nameArgumentBuilder;
    private readonly IOsTypeArgumentBuilder _osTypeArgumentBuilder;
    private readonly IOsVersionArgumentBuilder _osVersionArgumentBuilder;
    private readonly IRamMbArgumentBuilder _ramMbArgumentBuilder;
    private readonly IDefineMachineTypeAddCommandRequestBuilderFactory _requestBuilderFactory;
    private readonly ITimeoutMsOptionBuilder _timeoutMsOptionBuilder;
    private readonly IVirtualCpusArgumentBuilder _virtualCpusArgumentBuilder;
    private readonly IVirtualRamMbOptionBuilder _virtualRamMbOptionBuilder;
    private readonly IWorkingDirectoryOptionBuilder _workingDirectoryOptionBuilder;

    public DefineMachineTypeAddCommandIntegration(
        IDefineMachineTypeAddCommand command,
        IDefineMachineTypeAddCommandRequestBuilderFactory responseBuilderFactory,
        IMachineTypeDefinitionBuilderFactory machineTypeDefinitionBuilderFactory,
        INameArgumentBuilder nameArgumentBuilder,
        IBoxNameArgumentBuilder boxNameArgumentBuilder,
        IVirtualCpusArgumentBuilder virtualCpusArgumentBuilder,
        IRamMbArgumentBuilder ramMbArgumentBuilder,
        IOsTypeArgumentBuilder osTypeArgumentBuilder,
        IOsVersionArgumentBuilder osVersionArgumentBuilder,
        IEnabledOptionBuilder enabledOptionBuilder,
        IEnabled3dOptionBuilder enabled3dOptionBuilder,
        IVirtualRamMbOptionBuilder virtualRamMbOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder
    )
    {
        _command = command;
        _requestBuilderFactory = responseBuilderFactory;
        _machineTypeDefinitionBuilder = machineTypeDefinitionBuilderFactory;
        _nameArgumentBuilder = nameArgumentBuilder;
        _boxNameArgumentBuilder = boxNameArgumentBuilder;
        _virtualCpusArgumentBuilder = virtualCpusArgumentBuilder;
        _ramMbArgumentBuilder = ramMbArgumentBuilder;
        _osTypeArgumentBuilder = osTypeArgumentBuilder;
        _osVersionArgumentBuilder = osVersionArgumentBuilder;
        _enabledOptionBuilder = enabledOptionBuilder;
        _enabled3dOptionBuilder = enabled3dOptionBuilder;
        _virtualRamMbOptionBuilder = virtualRamMbOptionBuilder;
        _timeoutMsOptionBuilder = timeoutMsOptionBuilder;
        _workingDirectoryOptionBuilder = workingDirectoryOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var nameArg = _nameArgumentBuilder.Build();
        var boxNameArg = _boxNameArgumentBuilder.Build();
        var vcpusArg = _virtualCpusArgumentBuilder.Build();
        var ramMbArg = _ramMbArgumentBuilder.Build();
        var osTypeArg = _osTypeArgumentBuilder.Build();
        var osVersionArg = _osVersionArgumentBuilder.Build();
        var isEnabledOpt = _enabledOptionBuilder.Build();
        var isEnabled3dOpt = _enabled3dOptionBuilder.Build();
        var vramMbOpt = _virtualRamMbOptionBuilder.Build();
        var timeoutMsOpt = _timeoutMsOptionBuilder.Build();
        var workingDirOpt = _workingDirectoryOptionBuilder.Build();

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
            vramMbOpt,
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
            vramMbOpt,
            timeoutMsOpt,
            workingDirOpt
        );

        command.SetHandler(async (
            DefineMachineTypeAddCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            if (payload.OsType == null)
                throw new ArgumentNullException(nameof(payload.OsType));

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
                    .WithVideoRamInMb(payload.VideoRamInMb)
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