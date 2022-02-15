using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;

public class StatusCommandRequestBuilderFactory : RootCommandRequestBuilderFactory, IStatusCommandRequestBuilderFactory
{
    public StatusCommandRequestBuilderFactory(
        IBaseCommandRequestBuilderFactory baseRequestBuilderFactory
    ) : base(baseRequestBuilderFactory)
    {
    }

    public IStatusCommandRequestBuilder Factory()
    {
        return new StatusCommandRequestBuilder(BaseRequestBuilderFactory);
    }
}