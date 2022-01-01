using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;

public interface IHaltCommandResponseBuilder : IRootCommandResponseBuilder
{
    IHaltCommandResponse Build();
}