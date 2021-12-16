using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;
using System.Text;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig
{
    public class SshConfigCommand : RootCommand, ISshConfigCommand
    {
        private readonly ISshConfigCommandResponseBuilderFactory _responseBuilderFactory;

        public SshConfigCommand(
            IProcessBuilder processExecutor,
            IFilesystem fileSystem,
            ISshConfigCommandResponseBuilderFactory responseBuilderFactory
        ) : base(processExecutor, fileSystem)
        {
            _responseBuilderFactory = responseBuilderFactory;
        }

        public ISshConfigCommandResponse StartProcess(ISshConfigCommandRequest request)
        {
            var responseBuilder = _responseBuilderFactory.Build();

            BuildAndStartProcess(
                request,
                responseBuilder,
                BuildArguments(request)
            );

            return responseBuilder.Build();
        }

        private static string BuildArguments(ISshConfigCommandRequest request)
        {
            return GetCliCommandName() + " " + BuildVagrantOptions(request) + " " + BuildVagrantArguments(request);
        }

        private static string GetCliCommandName()
        {
            return "ssh-config";
        }

        protected static string BuildVagrantOptions(ISshConfigCommandRequest request)
        {
            if (null == request)
            {
                throw new InvalidOperationException("request is null");
            }

            if (null == request.Base)
            {
                throw new InvalidOperationException("request.Base is null");
            }

            return new StringBuilder()
                .Append(!string.IsNullOrEmpty(request.Host) ? $" --host {request.Host}" : "")
                .Append(BuildRootVagrantOptions(request.Base))
                .ToString()
                ;
        }
        protected static string BuildVagrantArguments(ISshConfigCommandRequest request)
        {
            return request.NameOrId;
        }
    }
}
