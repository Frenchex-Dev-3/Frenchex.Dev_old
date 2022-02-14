using System.CommandLine;
using Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain;
using Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain;

public class Integration : IIntegration
{
    private readonly IEnumerable<IVosCommandIntegration> _vexCommands;

    public Integration(IEnumerable<IVosCommandIntegration> vexCommands)
    {
        _vexCommands = vexCommands;
    }

    public void Integrate(RootCommand rootCommand)
    {
        var command = new Command("vos", "Vos commands");
        rootCommand.Add(command);

        foreach (var vexCommand in _vexCommands)
            vexCommand.Integrate(command);
    }
}