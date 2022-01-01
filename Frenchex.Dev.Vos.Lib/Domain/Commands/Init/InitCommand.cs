using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Create;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Resources;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public class InitCommand : RootCommand, IInitCommand
{
    private readonly IConfigurationCreateAction _configurationActionCreate;
    private readonly IFilesystem _filesystem;
    private readonly IInitCommandResponseBuilderFactory _responseBuilderFactory;
    private readonly IVagrantfileResource _vagrantfileResource;

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
        if (!_filesystem.DirectoryExists(request.Base.WorkingDirectory))
            _filesystem.CreateDirectory(request.Base.WorkingDirectory);

        _vagrantfileResource.Copy(request.Base.WorkingDirectory);

        await _configurationActionCreate.Create(
            Path.Join(request.Base.WorkingDirectory, "config.json"),
            new Configuration.Configuration()
        );

        return _responseBuilderFactory
            .Factory()
            .Build();
    }
}