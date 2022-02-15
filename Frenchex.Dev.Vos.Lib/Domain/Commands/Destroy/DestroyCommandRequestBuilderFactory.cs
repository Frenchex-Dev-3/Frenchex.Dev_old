using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Destroy;

public class DestroyCommandRequestBuilderFactory : RootCommandRequestBuilderFactory,
    IDestroyCommandRequestBuilderFactory
{
    public DestroyCommandRequestBuilderFactory(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public IDestroyCommandRequestBuilder Factory()
    {
        return new DestroyCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}