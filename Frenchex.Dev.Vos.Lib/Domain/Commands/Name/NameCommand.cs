using Frenchex.Dev.Vos.Lib.Domain.Actions.Configuration.Load;
using Frenchex.Dev.Vos.Lib.Domain.Actions.Naming;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public class NameCommand : RootCommand, INameCommand
{
    private readonly INameCommandResponseBuilderFactory _responseBuilderFactory;

    public NameCommand(
        INameCommandResponseBuilderFactory responseBuilderFactory,
        IConfigurationLoadAction configurationLoadAction,
        IVexNameToVagrantNameConverter vexNameToVagrantNameConverter
    ) : base(configurationLoadAction, vexNameToVagrantNameConverter)
    {
        _responseBuilderFactory = responseBuilderFactory;
    }

    public async Task<INameCommandResponse> Execute(INameCommandRequest request)
    {
        var config = await ConfigurationLoad(request.Base.WorkingDirectory);

        return _responseBuilderFactory
            .Factory()
            .SetNames(NameToVagrantNameConverter.ConvertAll(request.Names, request.Base.WorkingDirectory, config))
            .Build();
    }
}