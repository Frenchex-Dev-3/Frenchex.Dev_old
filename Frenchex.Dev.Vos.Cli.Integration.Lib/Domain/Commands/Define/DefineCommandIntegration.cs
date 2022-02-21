using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define;

public class DefineCommandIntegration : IDefineCommandIntegration
{
    private readonly IEnumerable<IDefineSubCommandIntegration> _defineSubCommandIntegrations;

    public DefineCommandIntegration(
        IEnumerable<IDefineSubCommandIntegration> subDefineCommandsIntegrations
    )
    {
        _defineSubCommandIntegrations = subDefineCommandsIntegrations;
    }

    public void Integrate(Command rootCommand)
    {
        var rootDefineCommand = new Command("define", "Defining configuration");

        rootCommand.Add(rootDefineCommand);

        foreach (var item in _defineSubCommandIntegrations) item.Integrate(rootDefineCommand);
    }
}