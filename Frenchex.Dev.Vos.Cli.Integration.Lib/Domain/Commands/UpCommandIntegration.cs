using System.CommandLine;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Up;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public interface IUpCommandIntegration : IVexCommandIntegration
{
}

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
        var command = new Command("up", "Runs Vagrant up")
        {
            new Argument<string[]>("Names of Machine to Up"),
            new Option<bool>(new[] {"--provision"}, "Provision"),
            new Option<string[]>(new[] {"--provision-with"}, "Provision with"),
            new Option<bool>(new[] {"--destroy-on-error"}, "Destroy on error"),
            new Option<bool>(new[] {"--parallel"}, "Parallel"),
            new Option<string>(new[] {"--provider"}, () => ProviderEnum.virtualbox.ToString(), "Provider"),
            new Option<bool>(new[] {"--install-provider", "-i"}, "Install provider"),
            new Option<int>(new[] {"--timeoutms", "-t"}, () => 0, "TimeOut in ms"),
            new Option<string>(new[] {"--working-directory", "-w"}, () => Environment.CurrentDirectory,
                "Working Directory")
        };

        command.Handler = CommandHandler.Create(async (
            IEnumerable<string> names,
            bool provision,
            string[] provisionWith,
            bool destroyOnError,
            bool parallel,
            string provider,
            bool installProvider,
            int timeOutMiliseconds,
            string workingDirectory,
            IConsole console
        ) =>
        {
            var response = await _command
                    .Execute(_requestBuilderFactory.Factory()
                        .BaseBuilder
                        .UsingWorkingDirectory(workingDirectory)
                        .UsingTimeoutMiliseconds(timeOutMiliseconds)
                        .Parent<UpCommandRequestBuilder>()
                        .UsingNames(names.ToArray())
                        .WithProvision(provision)
                        .UsingProvisionWith(provisionWith)
                        .WithDestroyOnError(destroyOnError)
                        .WithParallel(parallel)
                        .UsingProvider(provider)
                        .WithInstallProvider(installProvider)
                        .Build()
                    )
                ;
            if (null == response.Response.Process.WrappedProcess
                || null == response.Response.ProcessExecutionResult.WaitForCompleteExit)
                throw new InvalidOperationException("missing response elements");

            response.Response.Process.WrappedProcess.OutputDataReceived += (sender, args) =>
            {
                if (args.Data != null) console.Out.Write(args.Data + "\r\n");
            };

            Console.CancelKeyPress += delegate
            {
                Console.WriteLine("Cancel key pressed. Cleaning...");
                response.Response.Process.Stop();
                Console.WriteLine("Exiting");
            };

            await response.Response.ProcessExecutionResult.WaitForCompleteExit;
        });

        rootCommand.AddCommand(command);
    }
}