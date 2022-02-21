using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Name;

public class NameCommandIntegration : ABaseCommandIntegration, INameCommandIntegration
{
    private readonly INameCommand _command;
    private readonly INamesArgumentBuilder _namesArgumentBuilder;
    private readonly INameCommandRequestBuilderFactory _requestBuilderFactory;
    private readonly ITimeoutMsOptionBuilder _timeoutMsOptionBuilder;
    private readonly IWorkingDirectoryOptionBuilder _workingDirectoryOptionBuilder;

    public NameCommandIntegration(
        INameCommand command,
        INameCommandRequestBuilderFactory requestBuilderFactory,
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
                    .WithNames(payload.Names!)
                    .Build()
                );

            foreach (var name in response.Names) ctx.Console.Write(name);
        }, binder);

        rootCommand.AddCommand(command);
    }
}