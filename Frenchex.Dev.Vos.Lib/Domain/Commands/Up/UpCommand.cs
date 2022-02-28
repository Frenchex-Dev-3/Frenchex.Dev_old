using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up;

public class UpCommand : RootCommand, IUpCommand
{
    private readonly IUpCommandResponseBuilderFactory _responseBuilderFactory;
    private readonly Vagrant.Lib.Domain.Commands.Up.IUpCommand _vagrantUpCommand;

    private readonly Vagrant.Lib.Domain.Commands.Up.IUpCommandRequestBuilderFactory
        _vagrantUpCommandRequestBuilderFactory;

    public UpCommand(
        IUpCommandResponseBuilderFactory responseBuilderFactory,
        IConfigurationLoadAction configurationLoadAction,
        IVexNameToVagrantNameConverter vexNameToVagrantNameConverter,
        Vagrant.Lib.Domain.Commands.Up.IUpCommand vagrantUpCommand,
        Vagrant.Lib.Domain.Commands.Up.IUpCommandRequestBuilderFactory vagrantUpCommandRequestBuilderFactory
    ) : base(configurationLoadAction, vexNameToVagrantNameConverter)
    {
        _responseBuilderFactory = responseBuilderFactory;
        _vagrantUpCommand = vagrantUpCommand;
        _vagrantUpCommandRequestBuilderFactory = vagrantUpCommandRequestBuilderFactory;
    }

    public async Task<IUpCommandResponse> Execute(IUpCommandRequest request)
    {
        var libRequest = _vagrantUpCommandRequestBuilderFactory.Factory()
            .BaseBuilder
            .UsingWorkingDirectory(request.Base.WorkingDirectory)
            .UsingTimeoutMiliseconds(request.Base.TimeoutInMiliSeconds)
            .Parent<Vagrant.Lib.Domain.Commands.Up.IUpCommandRequestBuilder>()
            .UsingNamesOrIds(
                MapNamesToVagrantNames(
                    request.Names,
                    request.Base.WorkingDirectory,
                    await ConfigurationLoad(request.Base.WorkingDirectory)
                )
            )
            .Build();

        var vagrantUpProcess = _vagrantUpCommand.StartProcess(libRequest);

        return _responseBuilderFactory.Factory()
            .WithUpResponse(vagrantUpProcess)
            .Build();
    }
}