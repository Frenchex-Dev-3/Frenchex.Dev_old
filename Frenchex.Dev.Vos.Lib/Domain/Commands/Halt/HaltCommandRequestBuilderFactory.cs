using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public class HaltCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, IHaltCommandRequestBuilderFactory
{
    public HaltCommandRequestBuilderFactory(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public IHaltCommandRequestBuilder Factory()
    {
        return new HaltCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}