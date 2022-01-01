using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;

public interface IStatusCommandResponseBuilderFactory : IRootCommandResponseBuilderFactory
{
    IStatusCommandResponseBuilder Build();
}