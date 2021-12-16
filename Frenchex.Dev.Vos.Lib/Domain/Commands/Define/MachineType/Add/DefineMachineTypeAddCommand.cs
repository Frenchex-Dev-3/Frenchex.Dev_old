using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Save;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add
{
    public class DefineMachineTypeAddCommand : RootCommand, IDefineMachineTypeAddCommand
    {
        private readonly IDefineMachineTypeAddCommandResponseBuilderFactory _defineMachineTypeAddCommandResponseBuilderFactory;
        private readonly IConfigurationSaveAction _configurationSaveAction;
        public DefineMachineTypeAddCommand(
            IConfigurationLoadAction configurationLoadAction,
            IConfigurationSaveAction configurationSaveAction,
            IDefineMachineTypeAddCommandResponseBuilderFactory defineMachineTypeAddCommandResponseBuilderFactory,
            IVexNameToVagrantNameConverter nameConverter
        ) : base(configurationLoadAction, nameConverter)
        {
            _configurationSaveAction = configurationSaveAction;
            _defineMachineTypeAddCommandResponseBuilderFactory = defineMachineTypeAddCommandResponseBuilderFactory;
        }

        public async Task<IDefineMachineTypeAddCommandResponse> Execute(IDefineMachineTypeAddCommandRequest request)
        {
            var configFilePath = Path.Join(request.Base.WorkingDirectory, "config.json");
            var config = await _configurationLoadAction.Load(configFilePath);

            config.MachineTypes.Add(request.Definition.Name, request.Definition);

            await _configurationSaveAction.Save(config, configFilePath);

            return _defineMachineTypeAddCommandResponseBuilderFactory
                .Factory()
                .Build();
        }
    }
}
