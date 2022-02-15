using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;

public interface IHaltCommandRequestBuilder : IRootCommandRequestBuilder
{
    IHaltCommandRequest Build();
    IHaltCommandRequestBuilder UsingNamesOrIds(string[] namesOrIds);
    IHaltCommandRequestBuilder UsingHaltTimeoutInMiliSeconds(int timeoutMs);
}