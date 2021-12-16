using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig
{
    public interface ISshConfigCommandRequestBuilder : IRootCommandRequestBuilder
    {
        ISshConfigCommandRequest Build();
        ISshConfigCommandRequestBuilder UsingName(string nameOrId);
        ISshConfigCommandRequestBuilder UsingHost(string host);
    }

}
