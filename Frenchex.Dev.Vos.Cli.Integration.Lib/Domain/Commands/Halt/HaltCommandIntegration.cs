using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Halt;

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
        var namesArg = new Argument<string[]>("names", "Names or IDs");
        var forceOpt = new Option<bool>(new[] {"--force", "-f"}, "Force");
        var haltTimeoutMsOpt = new Option<int>(new[] {"--halt-timeoutms"},
            () => (int) TimeSpan.FromMinutes(1).TotalMilliseconds,
            "Halt timeout in ms");
        var timeoutMsOpt = new Option<int>(new[] {"--timeout-ms", "-t"}, "Timeout in ms");
        var workingDirOpt = new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory");

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
        ;

        rootCommand.AddCommand(command);
    }
}