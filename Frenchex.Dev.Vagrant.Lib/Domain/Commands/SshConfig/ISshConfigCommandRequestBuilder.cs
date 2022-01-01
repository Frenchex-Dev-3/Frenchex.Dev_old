using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;

public interface ISshConfigCommandRequestBuilder : IRootCommandRequestBuilder
{
    ISshConfigCommandRequest Build();
    ISshConfigCommandRequestBuilder UsingName(string nameOrId);
    ISshConfigCommandRequestBuilder UsingHost(string host);
}