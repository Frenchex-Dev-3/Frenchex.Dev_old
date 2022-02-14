using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Init;

public class InitCommandIntegration : ABaseCommandIntegration, IInitCommandIntegration
{
    private readonly IInitCommand _command;
    private readonly IInitCommandRequestBuilderFactory _responseBuilderFactory;

    public InitCommandIntegration(
        IInitCommand command,
        IInitCommandRequestBuilderFactory responseBuilderFactory
    )
    {
        _command = command;
        _responseBuilderFactory = responseBuilderFactory;
    }

    public void Integrate(Command rootCommand)
    {
        var namingOpt = new Option<string>(new[] {"--naming", "-n"}, () => "#{name}-#{instance}", "Naming pattern");
        var zeroesOpt = new Option<int>(new[] {"--zeroes", "-z"}, () => 2, "Numbering leading zeroes");
        var timeoutMsOpt = new Option<int>(new[] {"--timeout-ms", "-t"}, "TimeOut in ms");
        var workingDirOpt = new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory");

        var command = new Command("init", "Runs Vex init")
        {
            namingOpt,
            zeroesOpt,
            timeoutMsOpt,
            workingDirOpt
        };

        var customBinder = new InitCommandIntegrationPayloadBinder(
            namingOpt,
            zeroesOpt,
            timeoutMsOpt,
            workingDirOpt
        );

        command.SetHandler(async (
            InitCommandIntegrationPayload payload,
            InvocationContext ctx,
            HelpBuilder helpBuilder,
            CancellationToken cancellationToken
        ) =>
        {
            await _command
                    .Execute(_responseBuilderFactory.Factory()
                        .BaseBuilder
                        .UsingTimeoutMiliseconds(payload.TimeoutMs)
                        .UsingWorkingDirectory(payload.WorkingDirectory)
                        .Parent<IInitCommandRequestBuilder>()
                        .WithNamingPattern(payload.Naming)
                        .WithGivenLeadingZeroes(payload.Zeroes)
                        .Build()
                    )
                ;
        }, customBinder);

        rootCommand.AddCommand(command);
    }
}