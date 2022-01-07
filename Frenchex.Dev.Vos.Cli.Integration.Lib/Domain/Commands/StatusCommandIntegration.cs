using System.CommandLine;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public interface IStatusCommandIntegration : IVosCommandIntegration
{
}

public class StatusCommandIntegration : ABaseCommandIntegration, IStatusCommandIntegration
{
    private readonly IStatusCommand _command;
    private readonly IStatusCommandRequestBuilderFactory _requestBuilderFactory;

    public StatusCommandIntegration(
        IStatusCommand command,
        IStatusCommandRequestBuilderFactory requestBuilderFactory
    )
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
    }

    public void Integrate(Command rootCommand)
    {
        var command = new Command("status", "Runs Vagrant status")
        {
            new Argument<string[]>("names", "Names or IDs"),
            new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory"),
            new Option<int>(new[] {"--timeout", "-t"}, "TimeOut in ms")
        };

        command.Handler = CommandHandler.Create(async (
            string[] namesOrIds,
            string workingDirectory,
            int timeOutMiliseconds
        ) =>
        {
            await _command
                .Execute(_requestBuilderFactory.Factory()
                    .BaseBuilder
                    .UsingWorkingDirectory(workingDirectory)
                    .UsingTimeoutMiliseconds(timeOutMiliseconds)
                    .Parent<IStatusCommandRequestBuilder>()
                    .WithNames(namesOrIds)
                    .Build()
                );
        });

        rootCommand.AddCommand(command);
    }
}