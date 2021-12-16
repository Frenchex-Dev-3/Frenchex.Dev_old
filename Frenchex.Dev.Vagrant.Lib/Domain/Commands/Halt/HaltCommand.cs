using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Dotnet.Process.Lib.Domain.ProcessBuilder;
using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;
using System.Text;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt
{
    public class HaltCommand : RootCommand, IHaltCommand
    {
        private readonly IHaltCommandResponseBuilderFactory _responseBuilderFactory;

        public HaltCommand(
            IProcessBuilder processBuilder,
            IFilesystem fileSystem,
            IHaltCommandResponseBuilderFactory responseBuilderFactory
        ) : base(processBuilder, fileSystem)
        {
            _responseBuilderFactory = responseBuilderFactory;
        }

        public IHaltCommandResponse StartProcess(IHaltCommandRequest request)
        {
            var responseBuilder = _responseBuilderFactory.Build();

            BuildAndStartProcess(
                request,
                responseBuilder,
                BuildArguments(request)
            );

            return responseBuilder.Build();
        }

        private static string BuildArguments(IHaltCommandRequest request)
        {
            return GetCliCommandName() + " " + BuildVagrantOptions(request) + " " + BuildVagrantArguments(request);
        }

        private static string GetCliCommandName()
        {
            return "halt";
        }

        protected static string BuildVagrantOptions(IHaltCommandRequest request)
        {
            if (null == request.Base)
            {
                throw new InvalidOperationException("request.Base is null");
            }

            return new StringBuilder()
                .Append(request.Force ? " --force" : "")
                .Append(BuildRootVagrantOptions(request.Base))
                .ToString()
                ;
        }
        protected static string BuildVagrantArguments(IHaltCommandRequest request)
        {
            return request.NamesOrIds != null && request.NamesOrIds.Length > 0 ? String.Join(" ", request.NamesOrIds) : "";
        }

        protected static string BuildArguments(string command, IHaltCommandRequest request)
        {
            return
                   $"{command} " +
                   $"{BuildVagrantOptions(request)} " +
                   $"{BuildVagrantArguments(request)}"
                   ;
        }
    }
}
