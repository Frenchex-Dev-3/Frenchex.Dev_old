using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType;

public interface IDefineMachineTypeSubCommandIntegration
{
    void Integrate(Command rootDefineMachineCommand);
}