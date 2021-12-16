using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;
using System.Collections.Immutable;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status
{
    public class StatusCommand : RootCommand, IStatusCommand
    {
        private readonly IStatusCommandResponseBuilderFactory _responseBuilderFactory;

        public StatusCommand(
            IProcessBuilder processExecutor,
            IFilesystem fileSystem,
            IStatusCommandResponseBuilderFactory responseBuilderFactory
        ) : base(processExecutor, fileSystem)
        {
            _responseBuilderFactory = responseBuilderFactory;
        }

        public IStatusCommandResponse StartProcess(IStatusCommandRequest request)
        {
            var responseBuilder = _responseBuilderFactory.Build();

            var processExecution = BuildAndStartProcess(
                request,
                responseBuilder,
                BuildArguments(request)
            );

            if (null == processExecution)
            {
                throw new InvalidDataException("ProcessExecution");
            }

            if (null == processExecution.WaitForCompleteExit)
            {
                throw new InvalidDataException("WaitForCompleteExit");
            }

            if (null == processExecution.OutputStream)
            {
                throw new InvalidDataException("OutputStream");
            }

            return responseBuilder
                .Build();
        }

        private static string BuildArguments(IStatusCommandRequest request)
        {
            return $"{GetCliCommandName()} {string.Join(" ", request?.NamesOrIds ?? Array.Empty<string>())}";
        }

        private static string GetCliCommandName()
        {
            return "status";
        }
    }
}
