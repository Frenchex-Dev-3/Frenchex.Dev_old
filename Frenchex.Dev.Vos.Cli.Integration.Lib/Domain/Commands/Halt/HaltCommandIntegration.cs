using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Halt;

public class HaltCommandIntegration : ABaseCommandIntegration, IHaltCommandIntegration
{
    private readonly IHaltCommand _command;
    private readonly IForceOptionBuilder _forceOptionBuilder;
    private readonly INamesArgumentBuilder _namesArgumentBuilder;
    private readonly IHaltCommandRequestBuilderFactory _responseBuilderFactory;
    private readonly ITimeoutMsOptionBuilder _timeoutMsOptionBuilder;
    private readonly IWorkingDirectoryOptionBuilder _workingDirectoryOptionBuilder;

    public HaltCommandIntegration(
        IHaltCommand command,
        IHaltCommandRequestBuilderFactory responseBuilder,
        INamesArgumentBuilder namesArgumentBuilder,
        IForceOptionBuilder forceOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder
    )
    {
        _command = command;
        _responseBuilderFactory = responseBuilder;
        _namesArgumentBuilder = namesArgumentBuilder;
        _forceOptionBuilder = forceOptionBuilder;
        _timeoutMsOptionBuilder = timeoutMsOptionBuilder;
        _workingDirectoryOptionBuilder = workingDirectoryOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var namesArg = _namesArgumentBuilder.Build();
        var forceOpt = _forceOptionBuilder.Build();
        var haltTimeoutMsOpt = _timeoutMsOptionBuilder.Build(
            new[] {"--halt-timeoutms"},
            () => (int) TimeSpan.FromMinutes(1).TotalMilliseconds,
            "Halt timeout in ms"
        );
        var timeoutMsOpt = _timeoutMsOptionBuilder.Build();
        var workingDirOpt = _workingDirectoryOptionBuilder.Build();

        var command = new Command("halt", "Runs Vagrant halt")
        {
            namesArg,
            forceOpt,
            haltTimeoutMsOpt,
            timeoutMsOpt,
            workingDirOpt
        };

        var binder = new HaltCommandIntegrationPayloadBinder(
            namesArg,
            forceOpt,
            haltTimeoutMsOpt,
            timeoutMsOpt,
            workingDirOpt
        );

        command.SetHandler(async (
            HaltCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            await _command.Execute(
                    _responseBuilderFactory.Factory()
                        .BaseBuilder
                        .UsingTimeoutMiliseconds(payload.TimeoutMs)
                        .UsingWorkingDirectory(payload.WorkingDirectory)
                        .Parent<HaltCommandRequestBuilder>()
                        .UsingNames(payload.Names)
                        .WithForce(payload.Force)
                        .UsingHaltTimeoutInMiliSeconds(payload.HaltTimeoutMs)
                        .Build()
                )
                ;
        }, binder);

        rootCommand.AddCommand(command);
    }
}