using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;

public interface ISshCommandResponseBuilderFactory : IRootCommandResponseBuilderFactory
{
    ISshCommandResponseBuilder Build();
}