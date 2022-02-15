using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt;

public class HaltCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, IHaltCommandRequestBuilderFactory
{
    public HaltCommandRequestBuilderFactory(
        IBaseCommandRequestBuilderFactory baseRequestBuilderFactory
    ) : base(baseRequestBuilderFactory)
    {
    }

    public IHaltCommandRequestBuilder Factory()
    {
        return new HaltCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}