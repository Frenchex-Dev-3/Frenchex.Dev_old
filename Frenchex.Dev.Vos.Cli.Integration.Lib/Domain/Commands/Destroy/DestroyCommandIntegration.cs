using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Destroy;

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
        var nameOpt = new Argument<string[]>("name", "Name");
        var forceOpt = new Option<bool>(new[] {"--force", "-f"}, "Force");
        var parallelOpt = new Option<bool>(new[] {"--parallel", "-p"}, "Parallel");
        var gracefulOpt = new Option<bool>(new[] {"--graceful", "-g"}, "Graceful");
        var timeoutMsOpt = new Option<int>(new[] {"--timeout-ms", "-t"}, "TimeOut in ms");
        var workingDirOpt = new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory");

        var command = new Command("destroy", "Runs Vex destroy")
        {
            nameOpt,
            forceOpt,
            parallelOpt,
            gracefulOpt,
            timeoutMsOpt,
            workingDirOpt
        };

        var binder = new DestroyCommandIntegrationPayloadBinder(
            nameOpt,
            forceOpt,
            gracefulOpt,
            timeoutMsOpt,
            workingDirOpt
        );

        command.SetHandler(async (
            DestroyCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            await _command.Execute(_requestBuilderFactory.Factory()
                .BaseBuilder
                .UsingTimeoutMiliseconds(payload.TimeoutMs)
                .UsingWorkingDirectory(payload.WorkingDirectory)
                .Parent<DestroyCommandRequestBuilder>()
                .UsingName(payload.NameOrId.FirstOrDefault())
                .WithForce(payload.Force)
                .WithParallel(payload.Parallel)
                .WithGraceful(payload.Graceful)
                .Build()
            );
        }, binder);

        rootCommand.AddCommand(command);
    }
}