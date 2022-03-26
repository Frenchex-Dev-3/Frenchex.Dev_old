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

    public NameCommandIntegration(
        INameCommand command,
        INameCommandRequestBuilderFactory requestBuilderFactory,
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

        var command = new Command("name", "Output Vagrant machine names")
        {
            nameArg,
            timeoutOpt,
            workingDirOpt
        };

        var binder = new NameCommandIntegrationPayloadBinder(
            nameArg,
            timeoutOpt,
            workingDirOpt
        );

        command.SetHandler(async (
            NameCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            var requestBuilder = _requestBuilderFactory.Factory();

            BuildBase(requestBuilder, payload);

            var response = await _command.Execute(requestBuilder
                .WithNames(payload.Names!)
                .Build()
            );

            foreach (var name in response.Names) ctx.Console.Write(name);
        }, binder);

        rootCommand.AddCommand(command);
    }
}