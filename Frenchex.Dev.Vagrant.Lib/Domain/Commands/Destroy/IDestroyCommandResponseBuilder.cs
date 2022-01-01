using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;

public interface IDestroyCommandResponseBuilder : IRootCommandResponseBuilder
{
    IDestroyCommandResponse Build();
}