using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;
using System.Text;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh
{
    public class SshCommand : RootCommand, ISshCommand
    {
        private readonly ISshCommandResponseBuilderFactory _responseBuilderFactory;

        public SshCommand(
            IProcessBuilder processExecutor,
            IFilesystem fileSystem,
            ISshCommandResponseBuilderFactory responseBuilderFactory
        ) : base(processExecutor, fileSystem)
        {
            _responseBuilderFactory = responseBuilderFactory;
        }

        public ISshCommandResponse StartProcess(ISshCommandRequest request)
        {
            var responseBuilder = _responseBuilderFactory.Build();

            BuildAndStartProcess(
                request,
                responseBuilder,
                BuildArguments(request)
            );

            return responseBuilder.Build();
        }

        private static string BuildArguments(ISshCommandRequest request)
        {
            return GetCliCommandName() + " " + BuildVagrantOptions(request) + " " + BuildVagrantArguments(request);
        }

        protected static string BuildVagrantOptions(ISshCommandRequest request)
        {
            return new StringBuilder()
                .Append(!string.IsNullOrEmpty(request.Command) ? $" --command \"{request.Command}\"" : "")
                .Append(request.Plain ? $" --plain" : "")
                .Append(BuildRootVagrantOptions(request.Base))
                .ToString()
                ;
        }
        protected static string BuildVagrantArguments(ISshCommandRequest request)
        {
            return request.NameOrId;
        }

        private static string GetCliCommandName()
        {
            return "ssh";
        }
    }
}
