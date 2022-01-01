using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine;

public interface IDefineMachineCommandIntegration : IDefineSubCommandIntegration
{
}

public interface IDefineMachineSubCommandIntegration
{
    void Integrate(Command rootDefineMachineCommand);
}

public class DefineMachineCommandIntegration : IDefineMachineCommandIntegration
{
    private readonly IEnumerable<IDefineMachineSubCommandIntegration> _subs;

    public DefineMachineCommandIntegration(
        IEnumerable<IDefineMachineSubCommandIntegration> subs
    )
    {
        _subs = subs;
    }

    public void Integrate(Command rootDefineCommand)
    {
        var command = new Command("machine", "Machine definitionDeclaration commands");

        rootDefineCommand.Add(command);

        foreach (var item in _subs) item.Integrate(command);
    }
}