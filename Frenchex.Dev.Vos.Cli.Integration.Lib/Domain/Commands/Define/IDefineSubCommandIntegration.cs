using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define;

public interface IDefineSubCommandIntegration
{
    void Integrate(Command rootDefineCommand);
}