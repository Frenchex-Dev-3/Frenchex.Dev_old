using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandRequestBuilderFactory : RootCommandRequestBuilderFactory,
    ISshConfigCommandRequestBuilderFactory
{
    public SshConfigCommandRequestBuilderFactory(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public ISshConfigCommandRequestBuilder Factory()
    {
        return new SshConfigCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}