using System.CommandLine;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandIntegration : ABaseCommandIntegration, ISshConfigCommandIntegration
{
    private readonly ISshConfigCommand _command;
    private readonly INamesArgumentBuilder _namesArgumentBuilder;
    private readonly ISshConfigCommandRequestBuilderFactory _requestBuilderFactory;
    private readonly ITimeoutMsOptionBuilder _timeoutMsOptionBuilder;
    private readonly IWorkingDirectoryOptionBuilder _workingDirectoryOptionBuilder;

    public SshConfigCommandIntegration(
        ISshConfigCommand command,
        ISshConfigCommandRequestBuilderFactory requestBuilderFactory,
        INamesArgumentBuilder namesArgumentBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder
    )
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
        _namesArgumentBuilder = namesArgumentBuilder;
        _workingDirectoryOptionBuilder = workingDirectoryOptionBuilder;
        _timeoutMsOptionBuilder = timeoutMsOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var command = new Command("ssh-config", "Runs Vagrant ssh-config")
        {
            _namesArgumentBuilder.Build(),
            new Option<string>(new[] {"--host", "-h"}, "Host on guest"),
            _workingDirectoryOptionBuilder.Build(),
            _timeoutMsOptionBuilder.Build(),
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
                        .UsingWorkingDirectory(workingDirectory)
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