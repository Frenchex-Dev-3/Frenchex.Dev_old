using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Create;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Bases;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Resources;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init
{
    public class InitCommand : RootCommand, IInitCommand
    {
        private readonly IInitCommandResponseBuilderFactory _responseBuilderFactory;
        private readonly IVagrantfileResource _vagrantfileResource;
        private readonly IFilesystem _filesystem;
        private readonly IConfigurationCreateAction _configurationActionCreate;

        public InitCommand(
            IFilesystem fileSystemOperator,
            IVagrantfileResource vagrantfileResource,
            IInitCommandResponseBuilderFactory responseBuilderFactory,
            IConfigurationCreateAction configurationActionCreate,
            IConfigurationLoadAction configurationLoadAction,
            IVexNameToVagrantNameConverter nameConverter
        ) : base(configurationLoadAction, nameConverter)
        {
            _filesystem = fileSystemOperator;
            _vagrantfileResource = vagrantfileResource;
            _responseBuilderFactory = responseBuilderFactory;
            _configurationActionCreate = configurationActionCreate;
        }

        public async Task<IInitCommandResponse> Execute(IInitCommandRequest request)
        {
            var responseBuilder = _responseBuilderFactory.Factory();

            if (!_filesystem.DirectoryExists(request.Base.WorkingDirectory))
            {
                _filesystem.CreateDirectory(request.Base.WorkingDirectory);
            }

            _vagrantfileResource.Copy(request.Base.WorkingDirectory);

            Configuration config = new();

            await _configurationActionCreate.Create(
                Path.Join(request.Base.WorkingDirectory, "config.json"),
                config
            );

            return responseBuilder
                .Build();
        }
    }
}
