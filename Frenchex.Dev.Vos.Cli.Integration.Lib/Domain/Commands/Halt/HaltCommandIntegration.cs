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

    public HaltCommandIntegration(
        IHaltCommand command,
        IHaltCommandRequestBuilderFactory responseBuilder,
        INamesArgumentBuilder namesArgumentBuilder,
        IForceOptionBuilder forceOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder,
        IVagrantBinPathOptionBuilder vagrantBinPathOptionBuilder
    ) : base(workingDirectoryOptionBuilder, timeoutMsOptionBuilder, vagrantBinPathOptionBuilder)
    {
        _command = command;
        _responseBuilderFactory = responseBuilder;
        _namesArgumentBuilder = namesArgumentBuilder;
        _forceOptionBuilder = forceOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var namesArg = _namesArgumentBuilder.Build();
        var forceOpt = _forceOptionBuilder.Build();
        var haltTimeoutMsOpt = TimeoutMsOptionBuilder.Build(
            new[] {"--halt-timeoutms"},
            () => (int) TimeSpan.FromMinutes(1).TotalMilliseconds,
            "Halt timeout in ms"
        );
        var timeoutMsOpt = TimeoutMsOptionBuilder.Build();
        var workingDirOpt = WorkingDirectoryOptionBuilder.Build();
        var vagrantBinPath = VagrantBinPathOptionBuilder.Build();

        var command = new Command("halt", "Runs Vagrant halt")
        {
            namesArg,
            forceOpt,
            haltTimeoutMsOpt,
            timeoutMsOpt,
            workingDirOpt,
            vagrantBinPath
        };

        var binder = new HaltCommandIntegrationPayloadBinder(
            namesArg,
            forceOpt,
            haltTimeoutMsOpt,
            timeoutMsOpt,
            workingDirOpt,
            vagrantBinPath
        );

        command.SetHandler(async (
            HaltCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            var requestBuilder = _responseBuilderFactory.Factory();

            BuildBase(requestBuilder, payload);

            var response = await _command.Execute(requestBuilder
                    .UsingNames(payload.Names)
                    .WithForce(payload.Force)
                    .UsingHaltTimeoutInMiliSeconds(payload.HaltTimeoutMs)
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