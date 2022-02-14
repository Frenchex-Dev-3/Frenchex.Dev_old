using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.Machine;

public interface IDefineMachineSubCommandIntegration
{
    void Integrate(Command rootDefineMachineCommand);
}