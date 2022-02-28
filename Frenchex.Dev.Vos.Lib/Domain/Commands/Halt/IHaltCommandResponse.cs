using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public interface IHaltCommandResponse : IRootCommandResponse
{
    Vagrant.Lib.Domain.Commands.Halt.IHaltCommandResponse Response { get; }
}