using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Status;

public class StatusCommandIntegration : ABaseCommandIntegration, IStatusCommandIntegration
{
    private readonly IStatusCommand _command;
    private readonly INamesArgumentBuilder _namesArgumentBuilder;
    private readonly IStatusCommandRequestBuilderFactory _requestBuilderFactory;
    private readonly ITimeoutMsOptionBuilder _timeoutMsOptionBuilder;
    private readonly IWorkingDirectoryOptionBuilder _workingDirectoryOptionBuilder;

    public StatusCommandIntegration(
        IStatusCommand command,
        IStatusCommandRequestBuilderFactory requestBuilderFactory,
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
        var nameArg = _namesArgumentBuilder.Build();
        var workingDirOpt = _workingDirectoryOptionBuilder.Build();
        var timeoutOpt = _timeoutMsOptionBuilder.Build();


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
                    .WithNames(payload.Names!)
                    .Build()
                );
        }, binder);

        rootCommand.AddCommand(command);
    }
}