using System.CommandLine;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public interface ISshCommandIntegration : IVexCommandIntegration
{
}

public class SshCommandIntegration : ABaseCommandIntegration, ISshCommandIntegration
{
    private readonly ISshCommand _command;
    private readonly ISshCommandRequestBuilderFactory _requestBuilderFactory;

    public SshCommandIntegration(
        ISshCommand command,
        ISshCommandRequestBuilderFactory requestBuilderFactory
    )
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
    }

    public void Integrate(Command rootCommand)
    {
        var command = new Command("ssh", "Runs Vagrant ssh")
        {
            new Option<string[]>("--name", "Name or ID"),
            new Option<string>("--command", "Command"),
            new Option<string>(new[] {"--host", "-h"}, "Host on guest"),
            new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory"),
            new Option<int>(new[] {"--timeout", "-t"}, "TimeOut in ms")
        };

        command.Handler = CommandHandler.Create(async (
            string nameOrId,
            string sshCommand,
            string host,
            string workingDirectory,
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