using Frenchex.Dev.Dotnet.Filesystem.Lib.Domain;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Save;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Resources;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public class InitCommand : RootCommand, IInitCommand
{
    private readonly IConfigurationSaveAction _configurationActionSave;
    private readonly IFilesystem _filesystem;
    private readonly IInitCommandResponseBuilderFactory _responseBuilderFactory;
    private readonly IVagrantfileResource _vagrantfileResource;

    public InitCommand(
        IFilesystem fileSystemOperator,
        IVagrantfileResource vagrantfileResource,
        IInitCommandResponseBuilderFactory responseBuilderFactory,
        IConfigurationSaveAction configurationActionSave,
        IConfigurationLoadAction configurationLoadAction,
        IVexNameToVagrantNameConverter nameConverter
    ) : base(configurationLoadAction, nameConverter)
    {
        _filesystem = fileSystemOperator;
        _vagrantfileResource = vagrantfileResource;
        _responseBuilderFactory = responseBuilderFactory;
        _configurationActionSave = configurationActionSave;
    }

    public async Task<IInitCommandResponse> Execute(IInitCommandRequest request)
    {
        if (request.Base.WorkingDirectory == null)
            throw new ArgumentNullException(nameof(request.Base.WorkingDirectory));

        if (!_filesystem.DirectoryExists(request.Base.WorkingDirectory))
            _filesystem.CreateDirectory(request.Base.WorkingDirectory);

        _vagrantfileResource.Copy(request.Base.WorkingDirectory);

        await _configurationActionSave.Save(
            new Configuration.Configuration(),// @todo make it buildable via opts
            Path.Join(request.Base.WorkingDirectory, "config.json")
        );

        var provisioningPath = Path.GetFullPath("provisioning", request.Base.WorkingDirectory);
        var provisioningPathLink =
            Path.GetFullPath(Path.Join("Resources", "Provisioning"), AppDomain.CurrentDomain.BaseDirectory);

        _filesystem.CopyDirectory(provisioningPathLink, provisioningPath);

        return _responseBuilderFactory
        .Factory()
        .Build();
    }
}