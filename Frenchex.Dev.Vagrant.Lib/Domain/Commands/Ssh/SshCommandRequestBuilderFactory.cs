using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;

public class SshCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, ISshCommandRequestBuilderFactory
{
    public SshCommandRequestBuilderFactory(
        IBaseCommandRequestBuilderFactory baseRequestBuilderFactory
    ) : base(baseRequestBuilderFactory)
    {
    }

    public ISshCommandRequestBuilder Factory()
    {
        return new SshCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}