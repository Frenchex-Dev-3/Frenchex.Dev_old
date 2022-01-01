using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands;

public interface IVexCommandIntegration
{
    void Integrate(Command rootCommand);
}