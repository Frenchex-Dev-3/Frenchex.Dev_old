using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Save;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add
{
    public class DefineMachineAddCommand : RootCommand, IDefineMachineAddCommand
    {
        private readonly IDefineMachineAddCommandResponseBuilderFactory _responseBuilderFactory;
        private readonly IConfigurationSaveAction _configurationSaveAction;
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
            if (null == request?.Definition?.Name)
            {
                throw new InvalidOperationException("request or definition or name is null");
            }

            var configFilePath = Path.Join(request.Base.WorkingDirectory, "config.json");
            var config = await _configurationLoadAction.Load(configFilePath);

            config.Machines.Add(request.Definition.Name, request.Definition);

            await _configurationSaveAction.Save(config, configFilePath);

            return _responseBuilderFactory
                .Factory()
                .Build();
        }
    }
}
