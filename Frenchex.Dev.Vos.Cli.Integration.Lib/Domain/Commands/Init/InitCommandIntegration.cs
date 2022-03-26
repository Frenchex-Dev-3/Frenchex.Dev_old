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
    private readonly IZeroesOptionBuilder _zeroesOptionBuilder;

    public InitCommandIntegration(
        IInitCommand command,
        IInitCommandRequestBuilderFactory responseBuilderFactory,
        INamingPatternOptionBuilder namingPatternOptionBuilder,
        IZeroesOptionBuilder zeroesOptionBuilder,
        ITimeoutMsOptionBuilder? timeoutMsOptionBuilder,
        IWorkingDirectoryOptionBuilder? workingDirectoryOptionBuilder,
        IVagrantBinPathOptionBuilder? vagrantBinPathOptionBuilder
    ) : base(workingDirectoryOptionBuilder, timeoutMsOptionBuilder, vagrantBinPathOptionBuilder)
    {
        _command = command;
        _responseBuilderFactory = responseBuilderFactory;
        _namingPatternOptionBuilder = namingPatternOptionBuilder;
        _zeroesOptionBuilder = zeroesOptionBuilder;
    }

    public void Integrate(Command rootCommand)
    {
        var namingPatternOpt = _namingPatternOptionBuilder.Build();
        var zeroesOpt = _zeroesOptionBuilder.Build();
        var timeoutMsOpt = TimeoutMsOptionBuilder.Build();
        var workingDirOpt = WorkingDirectoryOptionBuilder.Build();

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
            var requestBuilder = _responseBuilderFactory.Factory();

            BuildBase(requestBuilder, payload);

            await _command.Execute(requestBuilder
                    .WithNamingPattern(payload.Naming)
                    .WithGivenLeadingZeroes(payload.Zeroes)
                    .Build()
                )
                ;
        }, customBinder);

        rootCommand.AddCommand(command);
    }
}