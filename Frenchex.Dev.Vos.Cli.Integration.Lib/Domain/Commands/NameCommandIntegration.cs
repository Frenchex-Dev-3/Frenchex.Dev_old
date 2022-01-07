using Frenchex.Dev.Vos.Lib.Domain.Commands.Name;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands
{
    public interface INameCommandIntegration : IVosCommandIntegration
    {

    }

    public class NameCommandIntegration : INameCommandIntegration
    {
        private readonly INameCommand _command;
        private readonly INameCommandRequestBuilderFactory _requestBuilderFactory;

        public NameCommandIntegration(
            INameCommand command,
            INameCommandRequestBuilderFactory requestBuilderFactory
        )
        {
            _command = command;
            _requestBuilderFactory = requestBuilderFactory;
        }

        public void Integrate(Command rootCommand)
        {
            var command = new Command("name", "Output Vagrant machine names")
            {
                new Argument<string[]>("Names"),
                new Option<string>(new[] {"--working-directory", "-w"}, "Working Directory"),
                new Option<int>(new[] {"--timeoutms", "-t"}, "TimeOut in ms")
            };

            command.Handler = CommandHandler.Create(async (
                string[] names,
                string workingDirectory,
                int timeOutMiliseconds
            ) =>
            {
                var response = await _command
                    .Execute(_requestBuilderFactory.Factory()
                        .BaseBuilder
                        .UsingWorkingDirectory(workingDirectory)
                        .UsingTimeoutMiliseconds(timeOutMiliseconds)
                        .Parent<INameCommandRequestBuilder>()
                        .WithNames(names)
                        .Build()
                    );

                Console.Write(response.Names);
            });

            rootCommand.AddCommand(command);
        }
    }
}
