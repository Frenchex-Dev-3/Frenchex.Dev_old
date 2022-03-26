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

    public StatusCommandIntegration(
        IStatusCommand command,
        IStatusCommandRequestBuilderFactory requestBuilderFactory,
        INamesArgumentBuilder namesArgumentBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder,
        IVagrantBinPathOptionBuilder vagrantBinPathOptionBuilder
    ) : base(workingDirectoryOptionBuilder, timeoutMsOptionBuilder, vagrantBinPathOptionBuilder)
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
        _namesArgumentBuilder = namesArgumentBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var nameArg = _namesArgumentBuilder.Build();
        var workingDirOpt = WorkingDirectoryOptionBuilder.Build();
        var timeoutOpt = TimeoutMsOptionBuilder.Build();
        var vagrantBinPath = VagrantBinPathOptionBuilder.Build();

        var command = new Command("status", "Runs Vagrant status")
        {
            nameArg,
            workingDirOpt,
            timeoutOpt,
            vagrantBinPath
        };

        var binder = new StatusCommandIntegrationPayloadBinder(nameArg, workingDirOpt, timeoutOpt);

        command.SetHandler(async (
            StatusCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            var requestBuilder = _requestBuilderFactory.Factory();

            BuildBase(requestBuilder, payload);

            await _command.Execute(requestBuilder
                .WithNames(payload.Names!)
                .Build()
            );
        }, binder);

        rootCommand.AddCommand(command);
    }
}