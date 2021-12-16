using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;
using System.Text;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up
{
    public class UpCommand : RootCommand, IUpCommand
    {
        private readonly IUpCommandResponseBuilderFactory _responseBuilderFactory;

        public UpCommand(
            IProcessBuilder processExecutor,
            IFilesystem fileSystem,
            IUpCommandResponseBuilderFactory responseBuilderFactory
        ) : base(processExecutor, fileSystem)
        {
            _responseBuilderFactory = responseBuilderFactory;
        }

        public IUpCommandResponse StartProcess(IUpCommandRequest request)
        {
            var responseBuilder = _responseBuilderFactory.Build();

            BuildAndStartProcess(
                request,
                responseBuilder,
                BuildArguments(request)
            );

            return responseBuilder.Build();
        }

        private static string BuildArguments(IUpCommandRequest request)
        {
            return GetCliCommandName() + " " + BuildVagrantOptions(request) + " " + BuildVagrantArguments(request);
        }

        private static string GetCliCommandName()
        {
            return "up";
        }

        protected static string BuildVagrantOptions(IUpCommandRequest request)
        {
            return new StringBuilder()
                .Append(request.Provision ? "" : " --no-provision")
                .Append(request.ProvisionWith?.Length > 0 ? " " + String.Join(' ', request.ProvisionWith) : "")
                .Append(request.DestroyOnError ? "" : " --no-destroy-on-error")
                .Append(request.Parallel ? "" : " --no-parallel")
                .Append(!string.IsNullOrEmpty(request.Provider) ? $" --provider {request.Provider}" : "")
                .Append(request.InstallProvider ? "" : " --no-install-provider")
                .Append(BuildRootVagrantOptions(request.Base))
                .ToString()
                ;
        }

        protected static string BuildVagrantArguments(IUpCommandRequest request)
        {
            return null == request?.NamesOrIds ?
                ""
                :
                String.Join(' ', request.NamesOrIds);
        }

        protected static string BuildArguments(string command, IUpCommandRequest request)
        {
            return
                   $"{command} " +
                   $"{BuildVagrantOptions(request)} " +
                   $"{BuildVagrantArguments(request)}"
                   ;
        }
    }
}
