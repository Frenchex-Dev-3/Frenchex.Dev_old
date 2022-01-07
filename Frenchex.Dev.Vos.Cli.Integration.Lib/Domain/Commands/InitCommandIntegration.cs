using System.CommandLine;
using System.CommandLine.Invocation;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public interface IInitCommandIntegration : IVosCommandIntegration
{
}

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
        var command = new Command("init", "Runs Vex init")
        {
            new Option<string>(new[] {"--naming", "-n"}, () => "#{name}-#{instance}", "Naming pattern"),
            new Option<int>(new[] {"--zeroes", "-z"}, () => 2, "Numbering leading zeroes"),
            new Option<int>(new[] {"--timeoutms", "-t"}, "TimeOut in ms"),
            new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory")
        };

        command.Handler = CommandHandler.Create(async (
            string naming,
            int zeroes,
            int timeOutMiliseconds,
            string workingDirectory
        ) =>
        {
            await _command
                    .Execute(_responseBuilderFactory.Factory()
                        .BaseBuilder
                        .UsingTimeoutMiliseconds(timeOutMiliseconds)
                        .UsingWorkingDirectory(workingDirectory)
                        .Parent<IInitCommandRequestBuilder>()
                        .WithNamingPattern(naming)
                        .WithGivenLeadingZeroes(zeroes)
                        .Build()
                    )
                ;
        });

        rootCommand.AddCommand(command);
    }
}