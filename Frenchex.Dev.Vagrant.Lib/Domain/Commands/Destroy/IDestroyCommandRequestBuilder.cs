using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Destroy;

public interface IDestroyCommandRequestBuilder : IRootCommandRequestBuilder
{
    IDestroyCommandRequestBuilder UsingName(string nameOrId);
    IDestroyCommandRequestBuilder WithForce(bool force);
    IDestroyCommandRequest Build();
}