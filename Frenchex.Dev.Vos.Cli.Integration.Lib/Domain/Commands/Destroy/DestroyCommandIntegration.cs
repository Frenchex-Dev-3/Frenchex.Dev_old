using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Destroy;

public class DestroyCommandIntegration : ABaseCommandIntegration, IDestroyCommandIntegration
{
    private readonly IDestroyCommand _command;
    private readonly IForceOptionBuilder _forceOptionBuilder;
    private readonly IGracefulOptionBuilder _gracefulOptionBuilder;
    private readonly INamesArgumentBuilder _namesArgumentBuilder;
    private readonly IParallelOptionBuilder _parallelOptionBuilder;
    private readonly IDestroyCommandRequestBuilderFactory _requestBuilderFactory;

    public DestroyCommandIntegration(
        IDestroyCommand command,
        IDestroyCommandRequestBuilderFactory responseBuilderFactory,
        INamesArgumentBuilder namesArgumentBuilder,
        IForceOptionBuilder forceOptionBuilder,
        IParallelOptionBuilder parallelOptionBuilder,
        IGracefulOptionBuilder gracefulOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder,
        IVagrantBinPathOptionBuilder vagrantBinPathOptionBuilder
    ) : base(workingDirectoryOptionBuilder, timeoutMsOptionBuilder, vagrantBinPathOptionBuilder)
    {
        _command = command;
        _requestBuilderFactory = responseBuilderFactory;
        _namesArgumentBuilder = namesArgumentBuilder;
        _forceOptionBuilder = forceOptionBuilder;
        _parallelOptionBuilder = parallelOptionBuilder;
        _gracefulOptionBuilder = gracefulOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var namesArg = _namesArgumentBuilder.Build();
        var forceOpt = _forceOptionBuilder.Build();
        var parallelOpt = _parallelOptionBuilder.Build();
        var gracefulOpt = _gracefulOptionBuilder.Build();
        var timeoutMsOpt = TimeoutMsOptionBuilder.Build();
        var workingDirOpt = WorkingDirectoryOptionBuilder.Build();
        var vagrantBinPath = VagrantBinPathOptionBuilder.Build();
        
        var command = new Command("destroy", "Runs Vex destroy")
        {
            namesArg,
            forceOpt,
            parallelOpt,
            gracefulOpt,
            timeoutMsOpt,
            workingDirOpt,
            vagrantBinPath
        };

        var binder = new DestroyCommandIntegrationPayloadBinder(
            namesArg,
            forceOpt,
            gracefulOpt,
            timeoutMsOpt,
            workingDirOpt,
            vagrantBinPath
        );

        command.SetHandler(async (
            DestroyCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            var requestBuilder = _requestBuilderFactory.Factory();

            BuildBase(requestBuilder, payload);

            await _command.Execute(requestBuilder
                .UsingName(payload.NameOrId!.FirstOrDefault())
                .WithForce(payload.Force)
                .WithParallel(payload.Parallel)
                .WithGraceful(payload.Graceful)
                .Build()
            );
        }, binder);

        rootCommand.AddCommand(command);
    }
}