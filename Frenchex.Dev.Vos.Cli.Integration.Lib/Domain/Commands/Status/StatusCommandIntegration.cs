using Frenchex.Dev.Vos.Lib.Domain.Commands.Status;
using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Status;

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
        var nameArg = new Argument<string[]>("Names");
        var workingDirOpt = new Option<string>(new[] { "--working-directory", "-w" }, "Working Directory");
        var timeoutOpt = new Option<int>(new[] { "--timeout-ms", "-t" }, "TimeOut in ms");


        var command = new Command("status", "Runs Vagrant status")
        {
            nameArg,
            workingDirOpt,
            timeoutOpt
        };

        var binder = new StatusCommandIntegrationPayloadBinder(nameArg, workingDirOpt, timeoutOpt);

        command.SetHandler(async (
            StatusCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            await _command
                .Execute(_requestBuilderFactory.Factory()
                    .BaseBuilder
                    .UsingWorkingDirectory(payload.WorkingDirectory)
                    .UsingTimeoutMiliseconds(payload.TimeoutMs)
                    .Parent<IStatusCommandRequestBuilder>()
                    .WithNames(payload.Names)
                    .Build()
                );
        }, binder);

        rootCommand.AddCommand(command);
    }
}