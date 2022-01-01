using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;

public interface IInitCommandResponseBuilderFactory : IRootCommandResponseBuilderFactory
{
    IInitCommandResponseBuilder Build();
}