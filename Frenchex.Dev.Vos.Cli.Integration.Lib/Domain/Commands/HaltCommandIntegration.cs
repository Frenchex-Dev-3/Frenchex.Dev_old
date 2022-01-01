using System.CommandLine;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public interface IHaltCommandIntegration : IVexCommandIntegration
{
}

public class HaltCommandIntegration : ABaseCommandIntegration, IHaltCommandIntegration
{
    private readonly IHaltCommand _command;
    private readonly IHaltCommandRequestBuilderFactory _responseBuilderFactory;

    public HaltCommandIntegration(
        IHaltCommand command,
        IHaltCommandRequestBuilderFactory responseBuilder
    )
    {
        _command = command;
        _responseBuilderFactory = responseBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var command = new Command("halt", "Runs Vagrant halt")
        {
            new Argument<string[]>("names", "Names or IDs"),
            new Option<bool>(new[] {"--force", "-f"}, "Force"),
            new Option<int>(new[] {"--halt-timeoutms"}, () => (int) TimeSpan.FromMinutes(1).TotalMilliseconds,
                "Halt timeout in ms"),
            new Option<int>(new[] {"--timeoutms", "-t"}, "Timeout in ms"),
            new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory")
        };

        command.Handler = CommandHandler.Create(async (
            string[] names,
            bool force,
            int haltTimeoutMs,
            int timeOutMiliseconds,
            string workingDirectory
        ) =>
        {
            await _command.Execute(
                    _responseBuilderFactory.Factory()
                        .BaseBuilder
                        .UsingTimeoutMiliseconds(timeOutMiliseconds)
                        .UsingWorkingDirectory(workingDirectory)
                        .Parent<HaltCommandRequestBuilder>()
                        .UsingNames(names)
                        .WithForce(force)
                        .UsingHaltTimeoutInMiliSeconds(haltTimeoutMs)
                        .Build()
                )
                ;
        });
        ;

        rootCommand.AddCommand(command);
    }
}