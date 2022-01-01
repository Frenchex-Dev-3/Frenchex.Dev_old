using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;

public interface
    IDefineMachineTypeAddCommand : IRootCommand<IDefineMachineTypeAddCommandRequest,
        IDefineMachineTypeAddCommandResponse>
{
}