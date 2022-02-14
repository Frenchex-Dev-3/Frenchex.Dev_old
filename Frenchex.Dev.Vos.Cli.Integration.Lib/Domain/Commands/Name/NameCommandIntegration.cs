using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Name;

public class NameCommandIntegration : ABaseCommandIntegration, INameCommandIntegration
{
    private readonly INameCommand _command;
    private readonly INameCommandRequestBuilderFactory _requestBuilderFactory;

    public NameCommandIntegration(
        INameCommand command,
        INameCommandRequestBuilderFactory requestBuilderFactory
    )
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
    }

    public void Integrate(Command rootCommand)
    {
        var nameArg = new Argument<string[]>("Names");
        var workingDirOpt = new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory");
        var timeoutOpt = new Option<int>(new[] {"--timeout-ms", "-t"}, "TimeOut in ms");

        var command = new Command("name", "Output Vagrant machine names")
        {
            nameArg,
            workingDirOpt,
            timeoutOpt
        };

        var binder = new NameCommandIntegrationPayloadBinder(
            nameArg,
            workingDirOpt,
            timeoutOpt
        );

        command.SetHandler(async (
            NameCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            var response = await _command
                .Execute(_requestBuilderFactory.Factory()
                    .BaseBuilder
                    .UsingWorkingDirectory(payload.WorkingDirectory)
                    .UsingTimeoutMiliseconds(payload.TimeoutMs)
                    .Parent<INameCommandRequestBuilder>()
                    .WithNames(payload.Names)
                    .Build()
                );

            foreach (var name in response.Names) ctx.Console.Write(name);
        }, binder);

        rootCommand.AddCommand(command);
    }
}