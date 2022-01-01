using System.CommandLine;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public interface IDestroyCommandIntegration : IVexCommandIntegration
{
}

public class DestroyCommandIntegration : ABaseCommandIntegration, IDestroyCommandIntegration
{
    private readonly IDestroyCommand _command;
    private readonly IDestroyCommandRequestBuilderFactory _requestBuilderFactory;

    public DestroyCommandIntegration(
        IDestroyCommand command,
        IDestroyCommandRequestBuilderFactory responseBuilderFactory
    )
    {
        _command = command;
        _requestBuilderFactory = responseBuilderFactory;
    }

    public void Integrate(Command rootCommand)
    {
        var command = new Command("destroy", "Runs Vex destroy")
        {
            new Argument<string[]>("names", "Names or IDs"),
            new Option<bool>(new[] {"--force", "-f"}, "Force"),
            new Option<bool>(new[] {"--parallel", "-p"}, "Parallel"),
            new Option<bool>(new[] {"--graceful", "-g"}, "Graceful"),
            new Option<int>(new[] {"--timeoutms", "-t"}, "TimeOut in ms"),
            new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory")
        };

        command.Handler = CommandHandler.Create(async (
            string nameOrId,
            bool force,
            bool parallel,
            bool gracefull,
            int timeOutMiliseconds,
            string workingDirectory
        ) =>
        {
            await _command.Execute(_requestBuilderFactory.Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds(timeOutMiliseconds)
                .UsingWorkingDirectory(workingDirectory)
                .Parent<DestroyCommandRequestBuilder>()
                .UsingName(nameOrId)
                .WithForce(force)
                .WithParallel(parallel)
                .WithGraceful(gracefull)
                .Build()
            );
        });

        rootCommand.AddCommand(command);
    }
}