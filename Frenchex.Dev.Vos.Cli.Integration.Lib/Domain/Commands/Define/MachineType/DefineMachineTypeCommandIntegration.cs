using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType;

public class DefineMachineTypeCommandIntegration : IDefineMachineTypeCommandIntegration
{
    private readonly IEnumerable<IDefineMachineTypeSubCommandIntegration> _subs;

    public DefineMachineTypeCommandIntegration(
        IEnumerable<IDefineMachineTypeSubCommandIntegration> subs
    )
    {
        _subs = subs;
    }

    public void Integrate(Command rootDefineCommand)
    {
        var command = new Command("machine-type", "Machine-type definitionDeclaration commands");

        rootDefineCommand.Add(command);

        foreach (var item in _subs) item.Integrate(command);
    }
}