using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Up;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Up;

public class UpCommandIntegration : ABaseCommandIntegration, IUpCommandIntegration
{
    private readonly IUpCommand _command;
    private readonly INamesArgumentBuilder _namesArgumentBuilder;
    private readonly IParallelOptionBuilder _parallelOptionBuilder;
    private readonly IUpCommandRequestBuilderFactory _requestBuilderFactory;
    private readonly ITimeoutMsOptionBuilder _timeoutMsOptionBuilder;
    private readonly IWorkingDirectoryOptionBuilder _workingDirectoryOptionBuilder;
    private readonly IParallelWorkersOptionBuilder _parallelWorkersOptionBuilder;
    private readonly IParallelWaitOptionBuilder _parallelWaitOptionBuilder;

    public UpCommandIntegration(
        IUpCommand command,
        IUpCommandRequestBuilderFactory requestBuilderFactory,
        INamesArgumentBuilder namesArgumentBuilder,
        IParallelOptionBuilder parallelOptionBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder,
        IParallelWorkersOptionBuilder parallelWorkersOptionBuilder,
        IParallelWaitOptionBuilder parallelWaitOptionBuilder)
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
        _namesArgumentBuilder = namesArgumentBuilder;
        _parallelOptionBuilder = parallelOptionBuilder;
        _workingDirectoryOptionBuilder = workingDirectoryOptionBuilder;
        _timeoutMsOptionBuilder = timeoutMsOptionBuilder;
        _parallelWorkersOptionBuilder = parallelWorkersOptionBuilder;
        _parallelWaitOptionBuilder = parallelWaitOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var namesArg = _namesArgumentBuilder.Build();
        var provisionOpt = new Option<bool>(new[] {"--provision"}, "Provision");
        var provisionWithOpt = new Option<string[]>(new[] {"--provision-with"}, "Provision with");
        var destroyOnErrorOpt = new Option<bool>(new[] {"--destroy-on-error"}, "Destroy on error");
        var parallelOpt = _parallelOptionBuilder.Build();
        var parallelWorkers = _parallelWorkersOptionBuilder.Build();
        var parallelWait = _parallelWaitOptionBuilder.Build();
        var providerOpt =
            new Option<string>(new[] {"--provider"}, () => ProviderEnum.Virtualbox.ToString(), "Provider");
        var installProviderOpt = new Option<bool>(new[] {"--install-provider", "-i"}, "Install provider");
        var timeoutMs = _timeoutMsOptionBuilder.Build();
        var workingDirOpt = _workingDirectoryOptionBuilder.Build();

        var command = new Command("up", "Runs Vagrant up")
        {
            namesArg,
            provisionOpt,
            provisionWithOpt,
            destroyOnErrorOpt,
            parallelOpt,
            parallelWorkers,
            parallelWait,
            providerOpt,
            installProviderOpt,
            timeoutMs,
            workingDirOpt
        };

        var binder = new UpCommandIntegrationPayloadBinder(
            namesArg,
            provisionOpt,
            provisionWithOpt,
            destroyOnErrorOpt,
            parallelOpt,
            parallelWorkers,
            parallelWait,
            providerOpt,
            installProviderOpt,
            timeoutMs,
            workingDirOpt
        );

        command.SetHandler(async (
            UpCommandIntegrationPayload payload,
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
                        .Parent<UpCommandRequestBuilder>()
                        .UsingNames(payload.Names!.ToArray())
                        .WithProvision(payload.Provision)
                        .UsingProvisionWith(payload.ProvisionWith!)
                        .WithDestroyOnError(payload.DestroyOnError)
                        .WithParallel(payload.Parallel)
                        .UsingProvider(payload.Provider!)
                        .WithInstallProvider(payload.InstallProvider)
                        .Build()
                    )
                ;

            if (null == response.Response.ProcessExecutionResult.WaitForCompleteExit)
                throw new InvalidOperationException("missing response elements");

            response.Response.Process.WrappedProcess.OutputDataReceived += (sender, args) =>
            {
                if (args.Data != null) ctx.Console.Out.Write(args.Data + "\r\n");
            };

            Console.CancelKeyPress += delegate
            {
                Console.WriteLine("Cancel key pressed. Cleaning...");
                response.Response.Process.Stop();
                Console.WriteLine("Exiting");
            };

            await response.Response.ProcessExecutionResult.WaitForCompleteExit;
        }, binder);

        rootCommand.AddCommand(command);
    }
}