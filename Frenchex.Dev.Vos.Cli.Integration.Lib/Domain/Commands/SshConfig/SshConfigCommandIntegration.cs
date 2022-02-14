using System.CommandLine;
using Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandIntegration : ABaseCommandIntegration, ISshConfigCommandIntegration
{
    private readonly ISshConfigCommand _command;
    private readonly ISshConfigCommandRequestBuilderFactory _requestBuilderFactory;

    public SshConfigCommandIntegration(
        ISshConfigCommand command,
        ISshConfigCommandRequestBuilderFactory requestBuilderFactory
    )
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
    }

    public void Integrate(Command rootCommand)
    {
        var command = new Command("ssh-config", "Runs Vagrant ssh-config")
        {
            new Argument<string[]>("name", "Name or ID"),
            new Option<string>(new[] {"--host", "-h"}, "Host on guest"),
            new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory"),
            new Option<int>(new[] {"--timeout-ms", "-t"}, "TimeOut in ms"),
            new Option<bool>(new[] {"--color"}, "Color")
        };

        command.SetHandler(async (
            string nameOrId,
            string host,
            string workingDirectory,
            int timeOutMiliseconds,
            bool withColor
        ) =>
        {
            await _command
                    .Execute(_requestBuilderFactory.Factory()
                        .BaseBuilder
                        .UsingTimeoutMiliseconds(timeOutMiliseconds)
                        .WithColor(withColor)
                        .Parent<SshConfigCommandRequestBuilder>()
                        .UsingName(nameOrId)
                        .UsingHost(host)
                        .Build()
                    )
                ;
        });

        rootCommand.AddCommand(command);
    }
}