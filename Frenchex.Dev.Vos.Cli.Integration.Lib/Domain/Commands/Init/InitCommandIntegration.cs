using System.CommandLine;
using System.CommandLine.Help;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Init;

public class InitCommandIntegration : ABaseCommandIntegration, IInitCommandIntegration
{
    private readonly IInitCommand _command;
    private readonly INamingPatternOptionBuilder _namingPatternOptionBuilder;
    private readonly IInitCommandRequestBuilderFactory _responseBuilderFactory;
    private readonly ITimeoutMsOptionBuilder _timeoutMsOptionBuilder;
    private readonly IWorkingDirectoryOptionBuilder _workingDirectoryOptionBuilder;
    private readonly IZeroesOptionBuilder _zeroesOptionBuilder;

    public InitCommandIntegration(
        IInitCommand command,
        IInitCommandRequestBuilderFactory responseBuilderFactory,
        IWorkingDirectoryOptionBuilder workingDirectoryOptionBuilder,
        INamingPatternOptionBuilder namingPatternOptionBuilder,
        IZeroesOptionBuilder zeroesOptionBuilder,
        ITimeoutMsOptionBuilder timeoutMsOptionBuilder
    )
    {
        _command = command;
        _responseBuilderFactory = responseBuilderFactory;
        _workingDirectoryOptionBuilder = workingDirectoryOptionBuilder;
        _namingPatternOptionBuilder = namingPatternOptionBuilder;
        _zeroesOptionBuilder = zeroesOptionBuilder;
        _timeoutMsOptionBuilder = timeoutMsOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var namingPatternOpt = _namingPatternOptionBuilder.Build();
        var zeroesOpt = _zeroesOptionBuilder.Build();
        var timeoutMsOpt = _timeoutMsOptionBuilder.Build();
        var workingDirOpt = _workingDirectoryOptionBuilder.Build();

        var command = new Command("init", "Runs Vex init")
        {
            namingPatternOpt,
            zeroesOpt,
            timeoutMsOpt,
            workingDirOpt
        };

        var customBinder = new InitCommandIntegrationPayloadBinder(
            namingPatternOpt,
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