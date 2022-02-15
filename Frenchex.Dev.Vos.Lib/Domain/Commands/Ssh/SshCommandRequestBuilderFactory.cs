using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

public class SshCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, ISshCommandRequestBuilderFactory
{
    public SshCommandRequestBuilderFactory(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public ISshCommandRequestBuilder Factory()
    {
        return new SshCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}