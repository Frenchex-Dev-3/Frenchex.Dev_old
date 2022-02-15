using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Up;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Up;

public class UpCommandIntegration : ABaseCommandIntegration, IUpCommandIntegration
{
    private readonly IUpCommand _command;
    private readonly IUpCommandRequestBuilderFactory _requestBuilderFactory;

    public UpCommandIntegration(
        IUpCommand command,
        IUpCommandRequestBuilderFactory requestBuilderFactory
    )
    {
        _command = command;
        _requestBuilderFactory = requestBuilderFactory;
    }

    public void Integrate(Command rootCommand)
    {
        var namesArg = new Argument<string[]>("names or IDs");
        var provisionOpt = new Option<bool>(new[] {"--provision"}, "Provision");
        var provisionWithOpt = new Option<string[]>(new[] {"--provision-with"}, "Provision with");
        var destroyOnErrorOpt = new Option<bool>(new[] {"--destroy-on-error"}, "Destroy on error");
        var parallelOpt = new Option<bool>(new[] {"--parallel"}, "Parallel");
        var providerOpt =
            new Option<string>(new[] {"--provider"}, () => ProviderEnum.Virtualbox.ToString(), "Provider");
        var installProviderOpt = new Option<bool>(new[] {"--install-provider", "-i"}, "Install provider");
        var timeoutMs = new Option<int>(new[] {"--timeout-ms", "-t"}, () => 0, "TimeOut in ms");
        var workiongDirOpt = new Option<string>(new[] {"--working-directory", "-w"}, () => Environment.CurrentDirectory,
            "Working Directory");

        var command = new Command("up", "Runs Vagrant up")
        {
            namesArg,
            provisionOpt,
            provisionWithOpt,
            destroyOnErrorOpt,
            parallelOpt,
            providerOpt,
            installProviderOpt,
            timeoutMs,
            workiongDirOpt
        };

        var binder = new UpCommandIntegrationPayloadBinder(
            namesArg,
            provisionOpt,
            provisionWithOpt,
            destroyOnErrorOpt,
            parallelOpt,
            providerOpt,
            installProviderOpt,
            timeoutMs,
            workiongDirOpt
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