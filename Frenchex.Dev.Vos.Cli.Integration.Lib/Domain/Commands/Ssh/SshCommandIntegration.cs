using System.CommandLine;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Ssh;

public class SshCommandIntegration : ABaseCommandIntegration, ISshCommandIntegration
{
    private readonly ISshCommand _command;
    private readonly INamesOptionBuilder _namesOptionBuilder;
    private readonly ISshCommandRequestBuilderFactory _requestBuilderFactory;

    public SshCommandIntegration(
        ISshCommand command,
        ISshCommandRequestBuilderFactory requestBuilderFactory,
        INamesOptionBuilder namesOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder,
        IVagrantBinPathOptionBuilder vagrantBinPathOptionBuilder
    ) : base(workingDirectoryOptionBuilder, timeoutMsOptionBuilder, vagrantBinPathOptionBuilder)
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
        _namesOptionBuilder = namesOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var command = new Command("ssh", "Runs Vagrant ssh")
        {
            _namesOptionBuilder.Build(),
            new Option<string>("--command", "Command"),
            new Option<string>(new[] {"--host", "-h"}, "Host on guest"),
            TimeoutMsOptionBuilder.Build(),
            VagrantBinPathOptionBuilder.Build(),
            WorkingDirectoryOptionBuilder.Build()
        };

        command.SetHandler(async (
            string nameOrId,
            string sshCommand,
            string host,
            int timeOutMiliseconds,
            string? workingDirectory,
            string? vagrantBinPath
        ) =>
        {
            var requestBuilder = _requestBuilderFactory.Factory();

            await _command
                .Execute(requestBuilder
                    .BaseBuilder
                    .UsingWorkingDirectory(workingDirectory)
                    .UsingTimeoutMiliseconds(timeOutMiliseconds)
                    .UsingVagrantBinPath(vagrantBinPath)
                    .Parent<SshCommandRequestBuilder>()
                    .UsingCommand(sshCommand)
                    .UsingName(nameOrId)
                    .Build()
                );
        });

        rootCommand.AddCommand(command);
    }
}