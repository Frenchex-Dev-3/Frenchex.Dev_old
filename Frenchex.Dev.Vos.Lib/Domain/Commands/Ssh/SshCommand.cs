using System.Text;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

public class SshCommand : RootCommand, ISshCommand
{
    private readonly ISshCommandResponseBuilderFactory _responseBuilderFactory;
    private readonly Vagrant.Lib.Domain.Commands.Ssh.ISshCommand _vagrantSshCommand;

    private readonly Vagrant.Lib.Domain.Commands.Ssh.ISshCommandRequestBuilderFactory
        _vagrantSshCommandRequestBuilderFactory;

    public SshCommand(
        ISshCommandResponseBuilderFactory responseBuilderFactory,
        IConfigurationLoadAction configurationLoadAction,
        IVexNameToVagrantNameConverter nameConverter,
        Vagrant.Lib.Domain.Commands.Ssh.ISshCommand vagrantSshCommand,
        Vagrant.Lib.Domain.Commands.Ssh.ISshCommandRequestBuilderFactory sshCommandRequestBuilderFactory
    ) : base(configurationLoadAction, nameConverter)
    {
        _responseBuilderFactory = responseBuilderFactory;
        _vagrantSshCommand = vagrantSshCommand;
        _vagrantSshCommandRequestBuilderFactory = sshCommandRequestBuilderFactory;
    }

    public async Task<ISshCommandResponse> Execute(ISshCommandRequest request)
    {
        var response = _vagrantSshCommand.StartProcess(_vagrantSshCommandRequestBuilderFactory.Factory()
            .BaseBuilder
            .UsingTimeoutMiliseconds(request.Base.TimeoutInMiliSeconds)
            .UsingWorkingDirectory(request.Base.WorkingDirectory)
            .Parent<Vagrant.Lib.Domain.Commands.Ssh.ISshCommandRequestBuilder>()
            .UsingCommand(request.Command)
            .UsingName(
                MapNamesToVagrantNames(
                    new[] {request.Name},
                    request.Base.WorkingDirectory,
                    await ConfigurationLoad(request.Base.WorkingDirectory)
                )[0]
            )
            .Build()
        );


        if (null == response.ProcessExecutionResult.WaitForCompleteExit)
            throw new InvalidOperationException("wait for complete exit is null");


        await response.ProcessExecutionResult.WaitForCompleteExit;

        var output =
            Encoding.UTF8.GetString(response.ProcessExecutionResult.OutputStream?.ToArray() ?? Array.Empty<byte>());

        var responseBuilder = _responseBuilderFactory.Build();

        return responseBuilder.Build();
    }
}