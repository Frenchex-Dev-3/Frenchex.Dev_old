using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandRequestBuilderFactory : RootCommandRequestBuilderFactory,
    ISshConfigCommandRequestBuilderFactory
{
    public SshConfigCommandRequestBuilderFactory(
        IBaseCommandRequestBuilderFactory baseCommandRequestBuilderFactory
    ) : base(baseCommandRequestBuilderFactory)
    {
    }

    public ISshConfigCommandRequestBuilder Factory()
    {
        return new SshConfigCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}