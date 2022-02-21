using System.CommandLine;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Ssh;

public class SshCommandIntegration : ABaseCommandIntegration, ISshCommandIntegration
{
    private readonly ISshCommand _command;
    private readonly INamesOptionBuilder _namesOptionBuilder;
    private readonly ISshCommandRequestBuilderFactory _requestBuilderFactory;
    private readonly ITimeoutMsOptionBuilder _timeoutMsOptionBuilder;
    private readonly IWorkingDirectoryOptionBuilder _workingDirectoryOptionBuilder;

    public SshCommandIntegration(
        ISshCommand command,
        ISshCommandRequestBuilderFactory requestBuilderFactory,
        INamesOptionBuilder namesOptionBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder
    )
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
        _namesOptionBuilder = namesOptionBuilder;
        _workingDirectoryOptionBuilder = workingDirectoryOptionBuilder;
        _timeoutMsOptionBuilder = timeoutMsOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var command = new Command("ssh", "Runs Vagrant ssh")
        {
            _namesOptionBuilder.Build(),
            new Option<string>("--command", "Command"),
            new Option<string>(new[] {"--host", "-h"}, "Host on guest"),
            _workingDirectoryOptionBuilder.Build(),
            _timeoutMsOptionBuilder.Build()
        };

        command.SetHandler(async (
            string nameOrId,
            string sshCommand,
            string host,
            string? workingDirectory,
            int timeOutMiliseconds
        ) =>
        {
            await _command
                .Execute(_requestBuilderFactory.Factory()
                    .BaseBuilder
                    .UsingWorkingDirectory(workingDirectory)
                    .UsingTimeoutMiliseconds(timeOutMiliseconds)
                    .Parent<SshCommandRequestBuilder>()
                    .UsingCommand(sshCommand)
                    .UsingName(nameOrId)
                    .Build()
                );
        });

        rootCommand.AddCommand(command);
    }
}