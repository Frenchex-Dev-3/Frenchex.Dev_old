using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public interface IVosCommandIntegration
{
    void Integrate(Command rootCommand);
}