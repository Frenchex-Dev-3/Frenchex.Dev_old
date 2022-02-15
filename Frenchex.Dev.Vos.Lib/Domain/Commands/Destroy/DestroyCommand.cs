using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

public class DestroyCommand : RootCommand, IDestroyCommand
{
    private readonly IDestroyCommandResponseBuilderFactory _responseBuilderFactory;
    private readonly Vagrant.Lib.Domain.Commands.Destroy.IDestroyCommand _vagrantDestroyCommand;

    private readonly Vagrant.Lib.Domain.Commands.Destroy.IDestroyCommandRequestBuilderFactory
        _vagrantDestroyCommandRequestBuilderFactory;

    public DestroyCommand(
        IDestroyCommandResponseBuilderFactory responseBuilderFactory,
        IConfigurationLoadAction configurationLoadAction,
        IVexNameToVagrantNameConverter nameConverter,
        Vagrant.Lib.Domain.Commands.Destroy.IDestroyCommand destroyCommand,
        Vagrant.Lib.Domain.Commands.Destroy.IDestroyCommandRequestBuilderFactory destroyCommandRequestBuilderFactory
    ) : base(configurationLoadAction, nameConverter)
    {
        _responseBuilderFactory = responseBuilderFactory;
        _vagrantDestroyCommand = destroyCommand;
        _vagrantDestroyCommandRequestBuilderFactory = destroyCommandRequestBuilderFactory;
    }

    public async Task<IDestroyCommandResponse> Execute(IDestroyCommandRequest request)
    {
        var process = _vagrantDestroyCommand.StartProcess(_vagrantDestroyCommandRequestBuilderFactory.Factory()
            .BaseBuilder
            .UsingWorkingDirectory(request.Base.WorkingDirectory)
            .UsingTimeoutMiliseconds(request.DestroyTimeoutInMiliSeconds)
            .Parent<Vagrant.Lib.Domain.Commands.Destroy.IDestroyCommandRequestBuilder>()
            .UsingName(
                !string.IsNullOrEmpty(request.Name)
                    ? MapNamesToVagrantNames(
                        new[] {request.Name},
                        request.Base.WorkingDirectory,
                        await ConfigurationLoad(request.Base.WorkingDirectory)
                    )[0]
                    : ""
            )
            .WithForce(true)
            .Build()
        );

        if (null == process.ProcessExecutionResult.WaitForCompleteExit)
            throw new InvalidOperationException("waitforcompleteexit is null");

        await process.ProcessExecutionResult.WaitForCompleteExit;

        var responseBuilder = _responseBuilderFactory.Build();

        return responseBuilder.Build();
    }
}