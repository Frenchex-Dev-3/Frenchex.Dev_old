using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;

public interface IDestroyCommandRequestBuilderFactory : IRootCommandRequestBuilderFactory
{
    IDestroyCommandRequestBuilder Factory();
}