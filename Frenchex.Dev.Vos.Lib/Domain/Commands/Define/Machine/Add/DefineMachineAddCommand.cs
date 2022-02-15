using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Save;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;

public class DefineMachineAddCommand : RootCommand, IDefineMachineAddCommand
{
    private readonly IConfigurationSaveAction _configurationSaveAction;
    private readonly IDefineMachineAddCommandResponseBuilderFactory _responseBuilderFactory;

    public DefineMachineAddCommand(
        IConfigurationLoadAction configurationLoadAction,
        IConfigurationSaveAction configurationSaveAction,
        IDefineMachineAddCommandResponseBuilderFactory responseBuilderFactory,
        IVexNameToVagrantNameConverter nameConverter
    ) : base(configurationLoadAction, nameConverter)
    {
        _configurationSaveAction = configurationSaveAction;
        _responseBuilderFactory = responseBuilderFactory;
    }

    public async Task<IDefineMachineAddCommandResponse> Execute(IDefineMachineAddCommandRequest request)
    {
        if (null == request.DefinitionDeclaration.Name)
            throw new InvalidOperationException("request or definitionDeclaration or name is null");

        var configFilePath = Path.Join(request.Base.WorkingDirectory, "config.json");
        var config = await ConfigurationLoadAction.Load(configFilePath);

        config.Machines.Add(request.DefinitionDeclaration.Name, request.DefinitionDeclaration);

        await _configurationSaveAction.Save(config, configFilePath);

        return _responseBuilderFactory
            .Factory()
            .Build();
    }
}