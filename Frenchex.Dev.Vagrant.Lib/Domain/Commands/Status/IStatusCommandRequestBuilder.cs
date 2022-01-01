using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;

public interface IStatusCommandRequestBuilder : IRootCommandRequestBuilder
{
    IStatusCommandRequest Build();
    IStatusCommandRequestBuilder WithNamesOrIds(string[] namesOrIds);
}