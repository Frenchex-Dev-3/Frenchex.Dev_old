using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

public class StatusCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, IStatusCommandRequestBuilderFactory
{
    public StatusCommandRequestBuilderFactory(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public IStatusCommandRequestBuilder Factory()
    {
        return new StatusCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}