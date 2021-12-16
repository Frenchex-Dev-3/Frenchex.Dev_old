namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig
{
    public interface ISshConfigCommandRequestBuilder : Root.IRootCommandRequestBuilder
    {
        ISshConfigCommandRequest Build();
        ISshConfigCommandRequestBuilder UsingName(string nameOrId);
        ISshConfigCommandRequestBuilder UsingHost(string host);
    }

}
