using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;

public interface ISshConfigCommandResponseBuilder : IRootCommandResponseBuilder
{
    ISshConfigCommandResponse Build();
}