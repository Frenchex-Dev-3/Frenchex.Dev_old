using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;

public interface ISshCommandResponseBuilder : IRootCommandResponseBuilder
{
    ISshCommandResponse Build();
}