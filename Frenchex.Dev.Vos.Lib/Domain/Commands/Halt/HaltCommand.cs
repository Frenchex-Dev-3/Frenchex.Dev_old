using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public class HaltCommand : RootCommand, IHaltCommand
{
    private readonly IHaltCommandResponseBuilderFactory _responseBuilderFactory;
    private readonly Vagrant.Lib.Domain.Commands.Halt.IHaltCommand _vagrantHaltCommand;

    private readonly Vagrant.Lib.Domain.Commands.Halt.IHaltCommandRequestBuilderFactory
        _vagrantHaltCommandRequestBuilderFactory;

    public HaltCommand(
        IHaltCommandResponseBuilderFactory responseBuilderFactory,
        IConfigurationLoadAction configurationLoadAction,
        IVexNameToVagrantNameConverter vexNameToVagrantNameConverter,
        Vagrant.Lib.Domain.Commands.Halt.IHaltCommand vagrantHaltCommand,
        Vagrant.Lib.Domain.Commands.Halt.IHaltCommandRequestBuilderFactory vagrantHaltCommandRequestBuilderFactory
    ) : base(configurationLoadAction, vexNameToVagrantNameConverter)
    {
        _responseBuilderFactory = responseBuilderFactory;
        _vagrantHaltCommand = vagrantHaltCommand;
        _vagrantHaltCommandRequestBuilderFactory = vagrantHaltCommandRequestBuilderFactory;
    }

    public async Task<IHaltCommandResponse> Execute(IHaltCommandRequest request)
    {
        var libRequest = _vagrantHaltCommandRequestBuilderFactory.Factory()
            .BaseBuilder
            .UsingWorkingDirectory(request.Base.WorkingDirectory)
            .UsingTimeoutMiliseconds(request.HaltTimeoutInMiliSeconds)
            .Parent<Vagrant.Lib.Domain.Commands.Halt.IHaltCommandRequestBuilder>()
            .UsingNamesOrIds(
                MapNamesToVagrantNames(
                    request.Names,
                    request.Base.WorkingDirectory,
                    await ConfigurationLoad(request.Base.WorkingDirectory)
                )
            )
            .UsingHaltTimeoutInMiliSeconds(request.HaltTimeoutInMiliSeconds)
            .Build();

        var process = _vagrantHaltCommand.StartProcess(libRequest);

        return _responseBuilderFactory.Factory()
            .WithHaltResponse(process)
            .Build();
    }
}